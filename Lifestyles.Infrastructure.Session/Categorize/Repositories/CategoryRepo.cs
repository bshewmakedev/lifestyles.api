using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Live.Repositories;
using CategoryMap = Lifestyles.Infrastructure.Session.Categorize.Map.Category;
using Lifestyles.Infrastructure.Session.Budget.Models;
using Lifestyles.Infrastructure.Session.Categorize.Models;
using Lifestyles.Infrastructure.Session.Live.Extensions;
using System.Data;
using Newtonsoft.Json;

namespace Lifestyles.Infrastructure.Session.Categorize.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly IKeyValueRepo _context;
        private readonly ILifestyleRepo _lifestyleRepo;

        public CategoryRepo(
            IKeyValueRepo context,
            ILifestyleRepo lifestyleRepo)
        {
            _context = context;
            _lifestyleRepo = lifestyleRepo;
        }

        private class CategoryJson
        {
            public string Label { get; set; }
        }
        public IEnumerable<ICategory> Default()
        {
            var lifestyles = _lifestyleRepo.Find();
            var dbCategories = new List<DbCategory>();
            foreach (var lifestyle in lifestyles)
            {
                using (StreamReader reader = File.OpenText($"../Lifestyles.Domain/Categorize/Defaults/Categories.{lifestyle.Label.Replace("-", "").Replace(" ", "")}.json"))
                {
                    dbCategories = dbCategories.Concat(JsonConvert
                        .DeserializeObject<List<CategoryJson>>(reader.ReadToEnd())
                        .Select(j => new DbCategory
                        {
                            Id = Guid.NewGuid(),
                            Label = j.Label
                        })).ToList();
                }
            }

            var categories = new List<ICategory>();
            var dbBudgetTypeDict = DbBudgetType.CreateDataTable(_context).GetRows().Select(r => new DbBudgetType(r)).ToDictionary(t => t.Alias, t => t.Id);
            var tableBudget = DbCategory.CreateDataTable(_context);
            var tableCategorized = DbCategorized.CreateDataTable(_context);
            dbCategories.ForEach(dbCategory =>
            {
                DbCategory.AddDataRow(tableBudget, dbBudgetTypeDict, dbCategory);
                categories.Add(new CategoryMap(dbCategory));
                lifestyles.ToList().ForEach(l => DbCategorized.AddDataRow(
                    tableCategorized, 
                    new DbCategorized { Id = Guid.NewGuid(), BudgetId = dbCategory.Id, CategoryId = l.Id }
                ));
            });
            _context.SetItem("tbl_Budget", tableBudget);
            _context.SetItem("tbl_Categorized", tableCategorized);

            return categories;
        }

        public IEnumerable<ICategory> Find(Func<ICategory, bool>? predicate = null)
        {
            var dbBudgetTypes = DbBudgetType.CreateDataTable(_context)
                .GetRows()
                .Select(r => new DbBudgetType(r));
            var categories = DbCategory.CreateDataTable(_context)
                .GetRows()
                .Select(r => new DbCategory(r))
                .Where(c => c.BudgetTypeId.Equals(
                    dbBudgetTypes.FirstOrDefault(bt => bt.Alias.Equals("category"))?.Id))
                .Select(c => new CategoryMap(c))
                .Where(predicate ?? ((b) => true));
            
            return categories;
        }

        public IEnumerable<ICategory> FindCategorizedAs(Guid categoryId)
        {
            var dbCategories = DbCategorized.CreateDataTable(_context)
                .GetRows()
                .Where(r => (r["CategoryId"]?.ToString() ?? "").Equals(categoryId.ToString()));
            var dbBudgetTypes = DbBudgetType.CreateDataTable(_context)
                .GetRows()
                .Select(r => new DbBudgetType(r));
            var categories = DbCategory.CreateDataTable(_context)
                .GetRows()
                .Select(r => new DbCategory(r))
                .Join(
                    dbCategories, 
                    b => b.Id, 
                    cr => Guid.Parse(cr["BudgetId"].ToString() ?? ""), (br, cr) => br)
                .Where(c => c.BudgetTypeId.Equals(
                    dbBudgetTypes.FirstOrDefault(bt => bt.Alias.Equals("category"))?.Id))
                .Select(c => new CategoryMap(c));
            
            return categories;
        }

        public IEnumerable<ICategory> Upsert(IEnumerable<ICategory> categories)
        {
            return categories;
        }

        public IEnumerable<ICategory> Remove(IEnumerable<ICategory> categories)
        {
            var categoriesDb = Find();

            return categoriesDb.Where(b => categories.All(b2 => !b.Id.Equals(b2.Id)));
        }
    }
}
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Live.Repositories;
using CategoryMap = Lifestyles.Infrastructure.Database.Categorize.Map.Category;
using Lifestyles.Infrastructure.Database.Budget.Models;
using Lifestyles.Infrastructure.Database.Categorize.Models;
using Lifestyles.Infrastructure.Database.Live.Extensions;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Categorize.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly IKeyValueStorage _context;
        private readonly ILifestyleRepo _lifestyleRepo;

        public CategoryRepo(
            IKeyValueStorage context,
            ILifestyleRepo lifestyleRepo)
        {
            _context = context;
            _lifestyleRepo = lifestyleRepo;
        }

        public IEnumerable<ICategory> Default()
        {
            var categories = new List<ICategory>();

            var lifestyles = _lifestyleRepo.Find();
            var dbBudgetTypeDict = DbBudgetType.CreateDataTable(_context).GetRows().Select(r => new DbBudgetType(r)).ToDictionary(t => t.Alias, t => t.Id);
            var tableBudget = DbCategory.CreateDataTable(_context);
            var tableCategorized = DbCategorized.CreateDataTable(_context);
            new List<DbCategory> {                
                new DbCategory { Id = Guid.NewGuid(), Label = "connect" },
                new DbCategory { Id = Guid.NewGuid(), Label = "eat & hydrate" },
                new DbCategory { Id = Guid.NewGuid(), Label = "hike" },
                new DbCategory { Id = Guid.NewGuid(), Label = "shelter" },
                new DbCategory { Id = Guid.NewGuid(), Label = "wear" }
            }.ForEach(dbCategory =>
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
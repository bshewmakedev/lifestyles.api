using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Categorize.Repositories;
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

        public CategoryRepo(IKeyValueStorage context)
        {
            _context = context;
        }

        public IEnumerable<ICategory> Find(Func<ICategory, bool>? predicate = null)
        {
            var dbBudgetTypes = _context.GetItem<DataTable>("tbl_BudgetType")
                .GetRows()
                .Select(r => new DbBudgetType(r));
            var categories = _context.GetItem<DataTable>("tbl_Budget")
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
            var dbCategories = _context.GetItem<DataTable>("tbl_Categorized")
                .GetRows()
                .Where(r => (r["CategoryId"]?.ToString() ?? "").Equals(categoryId.ToString()));

            var dbBudgetTypes = _context.GetItem<DataTable>("tbl_BudgetType")
                .GetRows()
                .Select(r => new DbBudgetType(r));
            var categories = _context.GetItem<DataTable>("tbl_Budget")
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
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Live.Comparers;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Session.Budget.Models;
using System.Linq;
using CategoryMap = Lifestyles.Infrastructure.Session.Categorize.Map.Category;

namespace Lifestyles.Infrastructure.Session.Categorize.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private List<JsonBudget> _jsonCategories
        { 
            get
            { 
                return _keyValueRepo
                    .GetItem<List<JsonBudget>>("tbl_Budget")
                    .Where(b => b.BudgetType.Equals("category"))
                    .ToList();
            }
            set
            {
                _keyValueRepo.SetItem("tbl_Budget", value);
            }
        }
        
        private readonly IKeyValueRepo _keyValueRepo;

        public CategoryRepo(IKeyValueRepo keyValueRepo)
        {
            _keyValueRepo = keyValueRepo;
        }

        public IEnumerable<ICategory> Find(Func<ICategory, bool>? predicate = null)
        {
            return _jsonCategories.Select(c => new CategoryMap(c));
        }

        public IEnumerable<ICategory> FindCategorizedAs(Guid categoryId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ICategory> Upsert(IEnumerable<ICategory> categories)
        {
            var categoriesMerged = categories
                .Except(
                    _jsonCategories.Select(c => new CategoryMap(c)),
                    new IdentifiedComparer<ICategory>());

            _jsonCategories = categoriesMerged.Select(c => new JsonBudget(c)).ToList();
            
            return categoriesMerged;
        }

        public IEnumerable<ICategory> Remove(IEnumerable<ICategory> categories)
        {
            var categoriesFiltered = _jsonCategories
                .Select(c => new CategoryMap(c))
                .Except(categories, new IdentifiedComparer<ICategory>());

            _jsonCategories = categoriesFiltered.Select(c => new JsonBudget(c)).ToList();
            
            return categoriesFiltered;
        }
    }
}
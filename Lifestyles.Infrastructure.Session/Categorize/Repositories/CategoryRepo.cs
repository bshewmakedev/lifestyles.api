using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Live.Comparers;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Session.Budget.Models;
using Lifestyles.Infrastructure.Session.Categorize.Comparers;
using Lifestyles.Infrastructure.Session.Categorize.Models;
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
                _keyValueRepo.SetItem("tbl_Budget", _keyValueRepo
                    .GetItem<List<JsonBudget>>("tbl_Budget")
                    .Where(b => !b.BudgetType.Equals("category"))
                    .Union(value)
                    .ToList());
            }
        }

        private List<JsonCategorize> _jsonCategorize
        {
            get
            {
                return _keyValueRepo.GetItem<List<JsonCategorize>>("tbl_Categorize");
            }
            set
            {
                _keyValueRepo.SetItem("tbl_Categorize", value);
            }
        }

        private readonly IKeyValueRepo _keyValueRepo;

        public CategoryRepo(IKeyValueRepo keyValueRepo)
        {
            _keyValueRepo = keyValueRepo;
        }

        public IEnumerable<ICategory> Find(Func<ICategory, bool>? predicate = null)
        {
            return _jsonCategories
                .Select(c => new CategoryMap(c))
                .Where(c => predicate == null ? true : predicate(c));
        }

        public IEnumerable<ICategory> FindCategorizedAs(Guid categoryId)
        {
            var jsonCategorize = _jsonCategorize;

            return Find(c => jsonCategorize.Contains(
                new JsonCategorize(c, categoryId),
                new CategorizeComparer()));
        }

        public IEnumerable<ICategory> Categorize(Guid categoryId, IEnumerable<ICategory> categories)
        {
            var categorizeMerged = categories
                .Select(c => new JsonCategorize(c, categoryId))
                .Union(_jsonCategorize, new EntityComparer())
                .ToList();
            
            _jsonCategorize = categorizeMerged;
            
            return FindCategorizedAs(categoryId);
        }

        public IEnumerable<ICategory> Upsert(IEnumerable<ICategory> categories)
        {
            var categoriesMerged = categories
                .Union(Find(), new IdentifiedComparer<ICategory>());

            _jsonCategories = categoriesMerged.Select(c => new JsonBudget(c)).ToList();

            return Find();
        }

        public IEnumerable<ICategory> Remove(IEnumerable<ICategory> categories)
        {
            var categoriesFiltered = Find().Except(categories, new IdentifiedComparer<ICategory>());

            _jsonCategories = categoriesFiltered.Select(c => new JsonBudget(c)).ToList();

            return Find();
        }
    }
}
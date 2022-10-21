using Lifestyles.Domain.Live.Comparers;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Session.Budget.Models;
using Lifestyles.Infrastructure.Session.Categorize.Comparers;
using Lifestyles.Infrastructure.Session.Categorize.Models;

namespace Lifestyles.Infrastructure.Session.Live.Repositories
{
    public abstract class EntityRepo<TEntity, TEntityMap>
        where TEntity : IIdentified
        where TEntityMap : TEntity, new()
    {
        private string _budgetType;
        private List<JsonBudget> _jsonBudgets
        {
            get
            {
                return _keyValueRepo
                    .GetItem<List<JsonBudget>>("tbl_Budget")
                    .Where(b => b.BudgetType.Equals(_budgetType))
                    .ToList();
            }
            set
            {
                _keyValueRepo.SetItem("tbl_Budget", _keyValueRepo
                    .GetItem<List<JsonBudget>>("tbl_Budget")
                    .Where(b => !b.BudgetType.Equals(_budgetType))
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

        public EntityRepo(
            IKeyValueRepo keyValueRepo,
            string budgetType)
        {
            _keyValueRepo = keyValueRepo;
            _budgetType = budgetType;
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool>? predicate = null)
        {
            return _jsonBudgets
                .Select(e => (TEntity)Activator.CreateInstance(typeof(TEntityMap), e))
                .Where(b => predicate == null ? true : predicate(b));
        }

        public IEnumerable<TEntity> FindCategorizedAs(Guid categoryId)
        {
            var jsonCategorize = _jsonCategorize;

            return Find(e => jsonCategorize.Contains(
                new JsonCategorize(e, categoryId),
                new CategorizeComparer()));
        }

        public IEnumerable<TEntity> Categorize(Guid categoryId, IEnumerable<TEntity> entities)
        {
            var categorizeMerged = entities
                .Select(b => new JsonCategorize(b, categoryId))
                .Union(_jsonCategorize, new EntityComparer())
                .ToList();

            _jsonCategorize = categorizeMerged;

            return FindCategorizedAs(categoryId);
        }

        public IEnumerable<TEntity> Upsert(IEnumerable<TEntity> entities)
        {
            var entitiesMerged = entities
                .Union(Find(), new IdentifiedComparer<TEntity>());

            _jsonBudgets = entitiesMerged.Select(e => (JsonBudget)Activator.CreateInstance(typeof(JsonBudget), e)).ToList();

            return Find();
        }

        public IEnumerable<TEntity> Remove(IEnumerable<TEntity> entities)
        {
            var entitiesFiltered = Find().Except(entities, new IdentifiedComparer<TEntity>());

            _jsonBudgets = entitiesFiltered.Select(e => (JsonBudget)Activator.CreateInstance(typeof(JsonBudget), e)).ToList();

            return Find();
        }
    }
}
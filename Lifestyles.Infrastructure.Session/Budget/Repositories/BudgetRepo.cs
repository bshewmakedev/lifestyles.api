using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Live.Comparers;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Session.Budget.Models;
using Lifestyles.Infrastructure.Session.Categorize.Comparers;
using Lifestyles.Infrastructure.Session.Categorize.Models;
using Newtonsoft.Json;
using BudgetMap = Lifestyles.Infrastructure.Session.Budget.Map.Budget;

namespace Lifestyles.Infrastructure.Session.Budget.Repositories
{
    public class BudgetRepo : IBudgetRepo
    {
        private List<JsonBudget> _jsonBudgets
        {
            get
            {
                return _keyValueRepo
                    .GetItem<List<JsonBudget>>("tbl_Budget")
                    .Where(b => b.BudgetType.Equals("budget"))
                    .ToList();
            }
            set
            {
                _keyValueRepo.SetItem("tbl_Budget", _keyValueRepo
                    .GetItem<List<JsonBudget>>("tbl_Budget")
                    .Where(b => !b.BudgetType.Equals("budget"))
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

        public BudgetRepo(IKeyValueRepo keyValueRepo)
        {
            _keyValueRepo = keyValueRepo;
        }

        public IEnumerable<IBudget> Find(Func<IBudget, bool>? predicate = null)
        {
            return _jsonBudgets
                .Select(b => new BudgetMap(b))
                .Where(b => predicate == null ? true : predicate(b));
        }

        public IEnumerable<IBudget> FindCategorizedAs(Guid categoryId)
        {
            var jsonCategorize = _jsonCategorize;

            Console.WriteLine("Current Categorize Suite:");
            Console.WriteLine(JsonConvert.SerializeObject(_jsonCategorize));

            return Find(b => jsonCategorize.Contains(
                new JsonCategorize(b, categoryId),
                new CategorizeComparer()));
        }

        public IEnumerable<IBudget> Categorize(Guid categoryId, IEnumerable<IBudget> budgets)
        {
            var categorizeMerged = budgets
                .Select(b => new JsonCategorize(b, categoryId))
                .Union(_jsonCategorize, new EntityComparer())
                .ToList();

            _jsonCategorize = categorizeMerged;

            return FindCategorizedAs(categoryId);
        }

        public IEnumerable<IBudget> Upsert(IEnumerable<IBudget> budgets)
        {
            var budgetsMerged = budgets
                .Union(Find(), new IdentifiedComparer<IBudget>());

            _jsonBudgets = budgetsMerged.Select(b => new JsonBudget(b)).ToList();

            return Find();
        }

        public IEnumerable<IBudget> Remove(IEnumerable<IBudget> budgets)
        {
            var budgetsFiltered = Find().Except(budgets, new IdentifiedComparer<IBudget>());

            _jsonBudgets = budgetsFiltered.Select(c => new JsonBudget(c)).ToList();

            return Find();
        }
    }
}
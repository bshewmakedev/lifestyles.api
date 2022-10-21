using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Live.Comparers;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Session.Budget.Models;
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
                _keyValueRepo.SetItem("tbl_Budget", value);
            }
        }

        private readonly IKeyValueRepo _keyValueRepo;

        public BudgetRepo(IKeyValueRepo keyValueRepo)
        {
            _keyValueRepo = keyValueRepo;
        }

        public IEnumerable<IBudget> Find(Func<IBudget, bool>? predicate = null)
        {
            return _jsonBudgets.Select(b => new BudgetMap(b));
        }

        public IEnumerable<IBudget> FindCategorizedAs(Guid categoryId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IBudget> Upsert(IEnumerable<IBudget> budgets)
        {
            var budgetsMerged = budgets
                .Except(
                    _jsonBudgets.Select(b => new BudgetMap(b)),
                    new IdentifiedComparer<IBudget>());

            _jsonBudgets = budgetsMerged.Select(b => new JsonBudget(b)).ToList();

            return budgetsMerged;
        }

        public IEnumerable<IBudget> Remove(IEnumerable<IBudget> budgets)
        {
            var budgetsFiltered = _jsonBudgets
                .Select(b => new BudgetMap(b))
                .Except(budgets, new IdentifiedComparer<IBudget>());

            _jsonBudgets = budgetsFiltered.Select(c => new JsonBudget(c)).ToList();
            
            return budgetsFiltered;
        }
    }
}
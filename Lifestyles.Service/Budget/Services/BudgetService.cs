using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Budget.Services;

namespace Lifestyles.Service.Budget.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepo _budgetRepo;

        public BudgetService(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public IEnumerable<IComparison<IBudget>> CompareBudgets(IEnumerable<IBudget> budgets)
        {
            throw new System.NotImplementedException();
        }
    }
}
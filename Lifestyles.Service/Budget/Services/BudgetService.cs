using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Services;

namespace Lifestyles.Service.Budget.Services
{
    public class BudgetService : IBudgetService
    {
        public IEnumerable<BudgetType> FindBudgetTypes()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IBudget> FindBudgets()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IBudget> FindBudgetsByCategoryId(Guid categoryId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IBudget> UpsertBudgets(IEnumerable<IBudget> budgets)
        {
            throw new System.NotImplementedException();
        }
        
        public IEnumerable<IBudget> RemoveBudgets(IEnumerable<IBudget> budgets)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IComparison<IBudget>> CompareBudgets(IEnumerable<IBudget> budgets)
        {
            throw new System.NotImplementedException();
        }
    }
}
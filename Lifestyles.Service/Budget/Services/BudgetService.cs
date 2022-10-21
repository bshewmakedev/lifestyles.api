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

        public IEnumerable<BudgetType> FindBudgetTypes()
        {
            return Enum.GetValues(typeof(BudgetType)).Cast<BudgetType>();
        }

        public IEnumerable<IBudget> FindBudgets()
        {
            return _budgetRepo.Find();
        }

        public IEnumerable<IBudget> FindBudgetsByCategoryId(Guid categoryId)
        {
            return _budgetRepo.FindCategorizedAs(categoryId);
        }

        public IEnumerable<IBudget> UpsertBudgets(IEnumerable<IBudget> budgets)
        {
            return _budgetRepo.Upsert(budgets);
        }
        
        public IEnumerable<IBudget> RemoveBudgets(IEnumerable<IBudget> budgets)
        {
            return _budgetRepo.Remove(budgets);
        }

        public IEnumerable<IComparison<IBudget>> CompareBudgets(IEnumerable<IBudget> budgets)
        {
            throw new System.NotImplementedException();
        }
    }
}
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Budget.Services;
using Lifestyles.Service.Budget.Models;
using Newtonsoft.Json;
using BudgetMap = Lifestyles.Service.Budget.Map.Budget;

namespace Lifestyles.Service.Budget.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepo _budgetRepo;

        public BudgetService(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public IEnumerable<IBudget> FindBudgets()
        {
            return _budgetRepo.Find();
        }

        public IEnumerable<IBudget> FindBudgetsByCategoryId(Guid categoryId)
        {
            return _budgetRepo.FindCategorizedAs(categoryId);
        }

        public IEnumerable<IBudget> CategorizeBudgets(Guid? categoryId, IEnumerable<IBudget> budgets)
        {
            return _budgetRepo.Categorize(categoryId, budgets);
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
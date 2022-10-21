using Lifestyles.Domain.Budget.Entities;

namespace Lifestyles.Domain.Budget.Services
{
    public interface IBudgetService
    {
        IEnumerable<IBudget> FindBudgets();
        IEnumerable<IBudget> FindBudgetsByCategoryId(Guid categoryId);
        IEnumerable<IBudget> CategorizeBudgets(Guid categoryId, IEnumerable<IBudget> budgets);
        IEnumerable<IBudget> UpsertBudgets(IEnumerable<IBudget> budgets);
        IEnumerable<IBudget> RemoveBudgets(IEnumerable<IBudget> budgets);
        IEnumerable<IComparison<IBudget>> CompareBudgets(IEnumerable<IBudget> budgets);
    }
}
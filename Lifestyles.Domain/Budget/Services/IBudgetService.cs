using Lifestyles.Domain.Budget.Entities;

namespace Lifestyles.Domain.Budget.Services
{
    public interface IBudgetService
    {
        IEnumerable<IComparison<IBudget>> CompareBudgets(IEnumerable<IBudget> budgets);
    }
}
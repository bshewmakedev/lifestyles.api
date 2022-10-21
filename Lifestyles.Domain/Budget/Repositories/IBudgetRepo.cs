using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Repositories;

namespace Lifestyles.Domain.Budget.Repositories
{
    public interface IBudgetRepo : IRepository<IBudget>
    { 
        IEnumerable<IBudget> FindCategorizedAs(Guid categoryId);
        IEnumerable<IBudget> Categorize(Guid categoryId, IEnumerable<IBudget> budgets);
    }
}
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Domain.Categorize.Entities
{
    public interface ICategory : IIdentified, ILabelled
    {
        decimal GetValue(
            IRecur recur,
            IEnumerable<IBudget> budgets,
            int? interval = null);
    }
}
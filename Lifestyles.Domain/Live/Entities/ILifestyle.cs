using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Constants;

namespace Lifestyles.Domain.Live.Entities
{
    public partial interface ILifestyle : ICategory
    {
        Existence Existence { get; }
        void Exist(Existence existence);
    }

    public partial interface ILifestyle
    {
        decimal? Lifetime { get; }
        Recurrence Recurrence { get; }
        decimal GetAmount(IEnumerable<IBudget> budgets, decimal? interval = null);
        Direction GetDirection(IEnumerable<IBudget> budgets);
        void Recur(Recurrence recurrence, decimal? lifetime = null);
    }
}
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Constants;

namespace Lifestyles.Domain.Live.Entities
{
    public partial interface ILifestyle : ICategory
    {
        int? Lifetime { get; }
        Recurrence Recurrence { get; }
        Existence Existence { get; }
        decimal GetAmount(IEnumerable<IBudget> budgets, int? interval = null);
        Direction GetDirection(IEnumerable<IBudget> budgets);
        void Recur(Recurrence recurrence = Recurrence.Never, int? lifetime = null);
        void Exist(Existence existence = Existence.Expected);
    }
}
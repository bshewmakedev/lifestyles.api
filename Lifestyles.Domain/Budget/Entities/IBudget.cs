using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Measure.Constants;

namespace Lifestyles.Domain.Budget.Entities
{
    public interface IBudget
    {
        decimal Amount { get; set; }
        IEnumerable<ICategory> Categories { get; set; }
        Direction Direction { get; set; }
        Existence Existence { get; set; }
        Guid Id { get; set; }
        string Label { get; set; }
        decimal? Lifetime { get; set; }
        Recurrence Recurrence { get; set; }
    }
}
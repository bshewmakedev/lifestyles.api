using Lifestyles.Domain.Measure.Constants;

namespace Lifestyles.Domain.Budget.Entities
{
    public interface IBudget
    {
        public decimal Amount { get; set; }
        public Direction Direction { get; set; }
        public Existence Existence { get; set; }
        public Guid Id { get; set; }
        public string Label { get; set; }
        public decimal? Lifetime { get; set; }
        public Recurrence Recurrence { get; set; }
    }
}
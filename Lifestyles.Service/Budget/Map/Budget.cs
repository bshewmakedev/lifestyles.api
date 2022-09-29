using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Measure.Constants;

namespace Lifestyles.Service.Budget.Map
{
    public class Budget : IBudget
    {
        public decimal Amount { get; set; } = 10;
        public Direction Direction { get; set; }
        public Existence Existence { get; set; } = Lifestyles.Domain.Measure.Constants.Existence.Excluded;
        public Guid Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public decimal? Lifetime { get; set; }
        public Recurrence Recurrence { get; set; }

        public Budget(Direction direction, Recurrence recurrence)
        {
            Direction = direction;
            Id = Guid.NewGuid();
            Lifetime =
                recurrence.Equals(Lifestyles.Domain.Measure.Constants.Recurrence.Never)
                ? Lifetime
                : 6;
            Recurrence = recurrence;
        }
    }
}
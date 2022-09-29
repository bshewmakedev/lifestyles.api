using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Measure.Constants;

namespace Lifestyles.Service.Live.Map
{
    public class Lifestyle : ILifestyle
    {
        public decimal Amount { get; set; } = 10;
        public IEnumerable<IBudget> Budgets { get; set; }
        public IEnumerable<ICategory> Categories { get; set; }
        public Direction Direction { get; set; }
        public Existence Existence { get; set; } = Lifestyles.Domain.Measure.Constants.Existence.Excluded;
        public Guid Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public decimal? Lifetime { get; set; }
        public Recurrence Recurrence { get; set; }

        public Lifestyle(Direction direction, Recurrence recurrence)
        {
            Budgets = new List<IBudget>();
            Categories = new List<ICategory>();
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
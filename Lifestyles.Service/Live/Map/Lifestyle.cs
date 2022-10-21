using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Service.Categorize.Map;
using DirectionEntity = Lifestyles.Domain.Live.Entities.Direction;
using RecurrenceEntity = Lifestyles.Domain.Live.Entities.Recurrence;
using ExistenceEntity = Lifestyles.Domain.Live.Entities.Existence;

namespace Lifestyles.Service.Live.Map
{
    public partial class Lifestyle : Category, ILifestyle
    {
        public int? Lifetime { get; private set; }
        public RecurrenceEntity Recurrence { get; private set; }
        public ExistenceEntity Existence { get; private set; }

        public Lifestyle(
            Guid? id = null,
            string label = "",
            int? lifetime = null,
            RecurrenceEntity recurrence = RecurrenceEntity.Never,
            ExistenceEntity existence = ExistenceEntity.Expected
        ) : base(id, label)
        {
            Recur(recurrence, lifetime);
            Exist(existence);
        }

        public decimal GetAmount(IEnumerable<IBudget> budgets, int? interval = null)
        {
            interval = interval ?? Lifetime - 1;

            if (budgets.Count() == 0) return 0;

            return budgets.Where(b => b.Existence.Equals(ExistenceEntity.Expected)).Select(b =>
            {
                var recurrenceToInt = (RecurrenceEntity r) =>
                {
                    switch (r)
                    {
                        case RecurrenceEntity.Daily: return 1;
                        case RecurrenceEntity.Weekly: return 7;
                        case RecurrenceEntity.Monthly: return 31;
                        case RecurrenceEntity.Annually: return 366;
                        default: return 0;
                    }
                };

                var recurrenceIntLifestyle = recurrenceToInt(Recurrence);
                var recurrenceIntBudget = recurrenceToInt(b.Recurrence);

                if (b.Recurrence.Equals(RecurrenceEntity.Never))
                {
                    return (int)b.Direction * b.Amount;
                }
                else
                {
                    return (int)b.Direction * (((interval + 1) * recurrenceIntLifestyle) / Math.Max(recurrenceIntBudget, 1) / Math.Max(b.Lifetime ?? 0, 1)) * b.Amount;
                }
            }).Sum() ?? 0;
        }

        public DirectionEntity GetDirection(IEnumerable<IBudget> budgets)
        {
            var sum = budgets.Sum(b => b.Amount * (int)b.Direction);

            if (sum > 0)
            {
                return DirectionEntity.In;
            }

            if (sum < 0)
            {
                return DirectionEntity.Out;
            }

            return DirectionEntity.Neutral;
        }

        public void Recur(RecurrenceEntity recurrence = RecurrenceEntity.Never, int? lifetime = null)
        {
            Recurrence = recurrence;

            if (recurrence.Equals(Lifestyles.Domain.Live.Entities.Recurrence.Never))
            {
                Lifetime = null;
            }
            else
            {
                Lifetime = lifetime;
            }
        }

        public void Exist(ExistenceEntity existence = ExistenceEntity.Expected)
        {
            Existence = existence;
        }
    }
}
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Constants;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Service.Categorize.Map;

namespace Lifestyles.Service.Live.Map
{
    public partial class Lifestyle : Category, ILifestyle
    {
        public int? Lifetime { get; private set; }
        public Recurrence Recurrence { get; private set; }
        public Existence Existence { get; private set; }

        public Lifestyle(
            Guid? id = null,
            string label = "",
            int? lifetime = null,
            Recurrence recurrence = Recurrence.Never,
            Existence existence = Existence.Expected
        ) : base(id, label)
        {
            Recur(recurrence, lifetime);
            Exist(existence);
        }

        public decimal GetAmount(IEnumerable<IBudget> budgets, int? interval = null)
        {
            interval = interval ?? Lifetime - 1;

            if (budgets.Count() == 0) return 0;

            //     const budgetsValidExpected = this.budgets.filter(
            //       (b) =>
            //         b.existence === Existence.Expected &&
            //         !isNaN(b.amount) &&
            //         ((b.isRecurring && b.recurrence && b.lifetime > 0) || !b.isRecurring)
            //     );

            //     const filteredBudgets = category ? category.filter(budgetsValidExpected) : budgetsValidExpected;

            return budgets.Where(b => b.Existence.Equals(Existence.Expected)).Select(b =>
            {
                var recurrenceToInt = (Recurrence r) => {
                    switch (r) {
                        case Recurrence.Daily: return 1;
                        case Recurrence.Weekly: return 7;
                        case Recurrence.Monthly: return 31;
                        case Recurrence.Annually: return 366;
                        default: return 0;
                    }
                };

                var directionInt = Lifestyles.Domain.Live.Map.Direction.Map(b.Direction);
                var recurrenceIntLifestyle = recurrenceToInt(Recurrence);
                var recurrenceIntBudget = recurrenceToInt(b.Recurrence);

                if (b.Recurrence.Equals(Recurrence.Never))
                {
                    return directionInt * b.Amount;
                }
                else
                {
                    return directionInt * (((interval + 1) * recurrenceIntLifestyle) / Math.Max(recurrenceIntBudget, 1) / Math.Max(b.Lifetime ?? 0, 1)) * b.Amount;   
                }
            }).Sum() ?? 0;
        }

        public Direction GetDirection(IEnumerable<IBudget> budgets)
        {
            var sum = budgets.Sum(b => b.Amount * Lifestyles.Domain.Live.Map.Direction.Map(b.Direction));

            return Lifestyles.Domain.Live.Map.Direction.Map((int)(sum / Math.Max(Math.Abs(sum), 1)));
        }

        public void Recur(Recurrence recurrence = Recurrence.Never, int? lifetime = null)
        {
            Recurrence = recurrence;

            if (recurrence.Equals(Lifestyles.Domain.Live.Constants.Recurrence.Never))
            {
                Lifetime = null;
            }
            else
            {
                Lifetime = lifetime;
            }
        }

        public void Exist(Existence existence = Existence.Expected)
        {
            Existence = existence;
        }
    }
}
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Constants;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Service.Categorize.Map;

namespace Lifestyles.Service.Live.Map
{
    public partial class Lifestyle : Category, ILifestyle
    {
        public Lifestyle(
            Guid? id = null,
            string label = "",
            decimal? lifetime = null,
            Recurrence recurrence = Recurrence.Never,
            Existence existence = Existence.Excluded
        ) : base(id, label)
        {
            Recur(recurrence, lifetime);
            Exist(existence);
        }
    }

    public partial class Lifestyle
    {
        public Existence Existence { get; private set; }

        public void Exist(Existence existence)
        {
            Existence = existence;
        }
    }

    public partial class Lifestyle
    {
        public decimal? Lifetime { get; private set; }
        public Recurrence Recurrence { get; private set; }

        public decimal GetAmount(IEnumerable<IBudget> budgets, decimal? interval = null)
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
                var directionInt = Lifestyles.Domain.Live.Map.Direction.Map(b.Direction);
                var recurrenceIntLifestyle = Lifestyles.Domain.Live.Map.Recurrence.Map(Recurrence);
                var recurrenceIntBudget = Lifestyles.Domain.Live.Map.Recurrence.Map(b.Recurrence);

                if (b.Recurrence.Equals(Recurrence.Never))
                {
                    var amountFragment = directionInt * b.Amount;
                    Console.WriteLine($"{amountFragment} on {b.Label}");
                    return amountFragment;
                }
                else
                {
                    var amountFragment = directionInt * b.Amount * (((interval + 1) * recurrenceIntLifestyle) / Math.Max(recurrenceIntBudget, 1) / Math.Max(b.Lifetime ?? 0, 1)); 
                    
                    Console.WriteLine($"{amountFragment} on {b.Label}, given interval {interval}, lifestyle recurrence {recurrenceIntLifestyle}, budget recurrence {recurrenceIntBudget}, budget lifetime {b.Lifetime}");
                    return amountFragment;
                }
            }).Sum() ?? 0;
        }

        public Direction GetDirection(IEnumerable<IBudget> budgets)
        {
            var sum = budgets.Sum(b => b.Amount * Lifestyles.Domain.Live.Map.Direction.Map(b.Direction));

            return Lifestyles.Domain.Live.Map.Direction.Map((int)(sum / Math.Max(Math.Abs(sum), 1)));
        }

        public void Recur(Recurrence recurrence, decimal? lifetime = null)
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
    }
}
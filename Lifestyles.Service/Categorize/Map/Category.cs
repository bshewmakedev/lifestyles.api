using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Service.Categorize.Models;

namespace Lifestyles.Service.Categorize.Map
{
    public partial class Category : ICategory
    {
        public Guid Id { get; set; }

        public void Identify(Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();
        }

        public string Label { get; set; } = string.Empty;

        public void Relabel(string label = "")
        {
            Label = label;
        }

        public decimal GetSignedAmount(
            IRecur recur,
            IEnumerable<IBudget> budgets,
            int? interval = null)
        {
            interval = interval ?? (recur.Lifetime.HasValue ? recur.Lifetime - 1 : 0);

            if (budgets.Where(b => b.Existence.Equals(Existence.Expected)).Count() == 0) return 0;

            var recurrenceToDecimal = (Recurrence r) =>
            {
                switch (r)
                {
                    case Recurrence.Daily: return 1.0m;
                    case Recurrence.Weekly: return 7.0m;
                    case Recurrence.Monthly: return 31.0m;
                    case Recurrence.Annually: return 366.0m;
                    default: return 0.0m;
                }
            };

            var recurDecimalLifestyle = recurrenceToDecimal(recur.Recurrence);
            var sum = budgets.Where(b => b.Existence.Equals(Existence.Expected)).Select(b =>
            {
                var recurDecimalBudget = recurrenceToDecimal(b.Recurrence);

                if (b.Recurrence.Equals(Recurrence.Never))
                {
                    return ((int)b.Direction) * b.Amount;
                }
                else
                {
                    var amount = ((int)b.Direction) * (((interval + 1) * recurDecimalLifestyle) / Math.Max(recurDecimalBudget, 1.0m) / Math.Max(b.Lifetime ?? 0.0m, 1.0m)) * b.Amount;

                    return amount;
                }
            }).Sum();

            return sum ?? 0;
        }

        public Category(
            Guid? id = null,
            string label = "")
        {
            Identify(id);
            Relabel(label);
        }

        public Category(DefaultCategory dfCategory)
        {
            Identify();
            Relabel(dfCategory.Label);
        }
    }
}
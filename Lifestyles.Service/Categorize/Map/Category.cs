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

            if (budgets.Count() == 0) return 0;

            return budgets.Where(b => b.Existence.Equals(Existence.Expected)).Select(b =>
            {
                var recurrenceToInt = (Recurrence r) =>
                {
                    switch (r)
                    {
                        case Recurrence.Daily: return 1;
                        case Recurrence.Weekly: return 7;
                        case Recurrence.Monthly: return 31;
                        case Recurrence.Annually: return 366;
                        default: return 0;
                    }
                };

                var recurrenceIntLifestyle = recurrenceToInt(recur.Recurrence);
                var recurrenceIntBudget = recurrenceToInt(b.Recurrence);

                if (b.Recurrence.Equals(Recurrence.Never))
                {
                    return (int)b.Direction * b.Amount;
                }
                else
                {
                    return (int)b.Direction * (((interval + 1) * recurrenceIntLifestyle) / Math.Max(recurrenceIntBudget, 1) / Math.Max(b.Lifetime ?? 0, 1)) * b.Amount;
                }
            }).Sum() ?? 0;
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
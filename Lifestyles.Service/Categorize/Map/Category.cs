using System;
using System.Collections.Generic;
using System.Linq;
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Node.Entities;

namespace Lifestyles.Service.Categorize.Map
{
    public class Category : ICategory, IEntity
    {
        public Guid Id { get; set; }

        public string Alias { get; set; } = string.Empty;

        public string Label { get; set; } = string.Empty;

        public IEntity Identify(Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();

            return this;
        }

        public IEntity As(string alias = "")
        {
            Alias = alias;

            return this;
        }

        public IEntity Relabel(string label = "")
        {
            Label = label;

            return this;
        }

        public decimal GetValue(
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
                    return b.Value;
                }
                else
                {
                    var value = (((interval + 1) * recurDecimalLifestyle) / Math.Max(recurDecimalBudget, 1.0m) / Math.Max(b.Lifetime ?? 0.0m, 1.0m)) * b.Value;

                    return value;
                }
            }).Sum();

            return sum ?? 0;
        }

        public Category() { }

        public Category(
            Guid? id = null,
            string alias = "",
            string label = "")
        {
            Identify(id);
            As(alias);
            Relabel(label);
        }
    }
}
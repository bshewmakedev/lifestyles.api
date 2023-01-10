using System;
using System.Collections.Generic;
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Service.Categorize.Map;

namespace Lifestyles.Service.Live.Map
{
    public class Lifestyle : Category, ILifestyle
    {
        public decimal GetValue(
            IEnumerable<IBudget> budgets,
            int? interval = null)
        {
            return base.GetValue(this, budgets, interval);
        }

        public int? Lifetime { get; private set; }
        public Lifestyles.Domain.Live.Entities.Recurrence Recurrence { get; private set; }

        public IRecur Recur(Lifestyles.Domain.Live.Entities.Recurrence recurrence = Lifestyles.Domain.Live.Entities.Recurrence.Never, int? lifetime = null)
        {
            Recurrence = recurrence;
            Lifetime = recurrence.Equals(Lifestyles.Domain.Live.Entities.Recurrence.Never) ? null : lifetime;

            return this;
        }

        public Lifestyles.Domain.Live.Entities.Existence Existence { get; private set; }

        public Lifestyles.Domain.Live.Entities.Existence Exist(Lifestyles.Domain.Live.Entities.Existence existence = Lifestyles.Domain.Live.Entities.Existence.Expected)
        {
            Existence = existence;

            return Existence;
        }

        public Lifestyle(
            Guid? id = null,
            string alias = "",
            string label = "",
            int? lifetime = null,
            Lifestyles.Domain.Live.Entities.Recurrence recurrence = Lifestyles.Domain.Live.Entities.Recurrence.Never,
            Lifestyles.Domain.Live.Entities.Existence existence = Lifestyles.Domain.Live.Entities.Existence.Expected
        ) : base(id, alias, label)
        {
            Recur(recurrence, lifetime);
            Exist(existence);
        }
    }
}
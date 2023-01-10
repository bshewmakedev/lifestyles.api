using System;
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Service.Live.Map;

namespace Lifestyles.Service.Budget.Map
{
    public class Budget : Lifestyle, IBudget
    {
        public decimal Value { get; private set; }
        public decimal Momentum { get; private set; }

        public IBudget Valuate(
            decimal value = 0.0m, 
            decimal momentum = 0.0m)
        {
            Value = value;
            Momentum = momentum;

            return this;
        }

        public Budget() { }

        public Budget(
            decimal value = 0.0m,
            decimal momentum = 0.0m,
            Guid? id = null,
            string alias = "",
            string label = "",
            int? lifetime = null,
            Lifestyles.Domain.Live.Entities.Recurrence recurrence = Lifestyles.Domain.Live.Entities.Recurrence.Never,
            Lifestyles.Domain.Live.Entities.Existence existence = Lifestyles.Domain.Live.Entities.Existence.Expected
        ) : base(id, alias, label, lifetime, recurrence, existence)
        {
            Valuate(value, momentum);
        }

        public Budget(IBudget budget) : base(
            budget.Id,
            budget.Alias,
            budget.Label,
            budget.Lifetime,
            budget.Recurrence,
            budget.Existence)
        {
            Valuate(budget.Value, budget.Momentum);
        }
    }
}
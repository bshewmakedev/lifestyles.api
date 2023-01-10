using System;
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Infrastructure.Session.Budget.Models
{
    public class JsonBudget
    {
        public Guid? Id { get; set; }
        public string Alias { get; set; }
        public string Label { get; set; }
        public decimal? Value { get; set; }
        public decimal? Momentum { get; set; }
        public int? Lifetime { get; set; }
        public string? Recurrence { get; set; }
        public string? Existence { get; set; }

        public JsonBudget() { }

        public JsonBudget(IBudget budget)
        {
            Id = budget.Id;
            Alias = budget.Alias;
            Label = budget.Label;
            Value = budget.Value;
            Momentum = budget.Momentum;
            Lifetime = budget.Lifetime;
            Recurrence = Lifestyles.Infrastructure.Session.Live.Models.Recurrence.Map(budget.Recurrence);
            Existence = Lifestyles.Infrastructure.Session.Live.Models.Existence.Map(budget.Existence);
        }

        public JsonBudget(ICategory category)
        {
            Id = category.Id;
            Alias = category.Alias;
            Label = category.Label;
            Value = null;
            Momentum = null;
            Lifetime = null;
            Recurrence = null;
            Existence = null;
        }

        public JsonBudget(ILifestyle lifestyle)
        {
            Id = lifestyle.Id;
            Alias = lifestyle.Alias;
            Label = lifestyle.Label;
            Value = null;
            Momentum = null;
            Lifetime = lifestyle.Lifetime;
            Recurrence = Lifestyles.Infrastructure.Session.Live.Models.Recurrence.Map(lifestyle.Recurrence);
            Existence = Lifestyles.Infrastructure.Session.Live.Models.Existence.Map(lifestyle.Existence);
        }
    }
}
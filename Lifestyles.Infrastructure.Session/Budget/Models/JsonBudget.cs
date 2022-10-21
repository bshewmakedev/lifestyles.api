using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Infrastructure.Session.Budget.Models
{
    public class JsonBudget
    {
        public Guid Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public string BudgetType { get; set; } = string.Empty;
        public decimal? Amount { get; set; }
        public int? Lifetime { get; set; }
        public string? Recurrence { get; set; }
        public string? Existence { get; set; }

        public JsonBudget(IBudget budget)
        {
            Id = budget.Id;
            Label = budget.Label;
            BudgetType = "budget";
            Amount = budget.Amount;
            Lifetime = budget.Lifetime;
            Recurrence = Lifestyles.Infrastructure.Session.Live.Models.JsonRecurrence.Map(budget.Recurrence);
            Existence = Lifestyles.Infrastructure.Session.Live.Models.JsonExistence.Map(budget.Existence);
        }

        public JsonBudget(ICategory category)
        {
            Id = category.Id;
            Label = category.Label;
            BudgetType = "category";
            Amount = null;
            Lifetime = null;
            Recurrence = null;
            Existence = null;
        }

        public JsonBudget(ILifestyle lifestyle)
        {
            Id = lifestyle.Id;
            Label = lifestyle.Label;
            BudgetType = "lifestyle";
            Amount = null;
            Lifetime = lifestyle.Lifetime;
            Recurrence = Lifestyles.Infrastructure.Session.Live.Models.JsonRecurrence.Map(lifestyle.Recurrence);
            Existence = Lifestyles.Infrastructure.Session.Live.Models.JsonExistence.Map(lifestyle.Existence);
        }
    }
}
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Entities;
using RecurrenceMap = Lifestyles.Domain.Live.Map.Recurrence;
using ExistenceMap = Lifestyles.Domain.Live.Map.Existence;

namespace Lifestyles.Infrastructure.Session.Budget.Models
{
    public class JsonBudget
    {
        public Guid Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public string NodeType { get; set; } = string.Empty;
        public decimal? Value { get; set; }
        public int? Lifetime { get; set; }
        public string? Recurrence { get; set; }
        public string? Existence { get; set; }

        public JsonBudget() { }

        public JsonBudget(IBudget budget)
        {
            Id = budget.Id;
            Label = budget.Label;
            NodeType = Lifestyles.Domain.Tree.Map.NodeType.Budget;
            Value = budget.Value;
            Lifetime = budget.Lifetime;
            Recurrence = RecurrenceMap.Map(budget.Recurrence);
            Existence = ExistenceMap.Map(budget.Existence);
        }

        public JsonBudget(ICategory category)
        {
            Id = category.Id;
            Label = category.Label;
            NodeType = Lifestyles.Domain.Tree.Map.NodeType.Category;
            Value = null;
            Lifetime = null;
            Recurrence = null;
            Existence = null;
        }

        public JsonBudget(ILifestyle lifestyle)
        {
            Id = lifestyle.Id;
            Label = lifestyle.Label;
            NodeType = Lifestyles.Domain.Tree.Map.NodeType.Lifestyle;
            Value = null;
            Lifetime = lifestyle.Lifetime;
            Recurrence = RecurrenceMap.Map(lifestyle.Recurrence);
            Existence = ExistenceMap.Map(lifestyle.Existence);
        }
    }
}
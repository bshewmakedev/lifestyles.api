using System;
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Infrastructure.Session.Live.Models;

namespace Lifestyles.Infrastructure.Session.Budget.Models
{
    public class JsonBudget : JsonLifestyle
    {
        public decimal? Value { get; set; }
        public decimal? Momentum { get; set; }

        public JsonBudget() { }

        public JsonBudget(IBudget budget) : base(budget)
        {
            Value = budget.Value;
            Momentum = budget.Momentum;
        }
    }
}
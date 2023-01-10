using Lifestyles.Infrastructure.Session.Budget.Models;

namespace Lifestyles.Infrastructure.Session.Budget.Map
{
    public class Budget : Lifestyles.Service.Budget.Map.Budget
    {
        public Budget() { }

        public Budget(JsonBudget jsonBudget) : base(
            jsonBudget.Value ?? default(decimal),
            jsonBudget.Momentum ?? default(decimal),
            jsonBudget.Id,
            jsonBudget.Alias,
            jsonBudget.Label,
            jsonBudget.Lifetime,
            Lifestyles.Infrastructure.Session.Live.Map.Recurrence.Map(jsonBudget.Recurrence),
            Lifestyles.Infrastructure.Session.Live.Map.Existence.Map(jsonBudget.Existence))
        { }
    }
}
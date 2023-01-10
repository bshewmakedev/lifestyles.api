using Lifestyles.Infrastructure.Session.Budget.Models;

namespace Lifestyles.Infrastructure.Session.Live.Map
{
    public class Lifestyle : Lifestyles.Service.Live.Map.Lifestyle
    {
        public Lifestyle() { }

        public Lifestyle(JsonBudget jsonBudget) : base(
            jsonBudget.Id,
            jsonBudget.Alias,
            jsonBudget.Label,
            jsonBudget.Lifetime,
            Lifestyles.Infrastructure.Session.Live.Map.Recurrence.Map(jsonBudget.Recurrence),
            Lifestyles.Infrastructure.Session.Live.Map.Existence.Map(jsonBudget.Existence))
        { }
    }
}
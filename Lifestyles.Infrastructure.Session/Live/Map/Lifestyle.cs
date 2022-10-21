using Lifestyles.Infrastructure.Session.Budget.Models;
using LifestyleMap = Lifestyles.Service.Live.Map.Lifestyle;

namespace Lifestyles.Infrastructure.Session.Live.Map
{
    public class Lifestyle : LifestyleMap
    {
        public Lifestyle(JsonBudget jsonBudget) : base(
            jsonBudget.Id,
            jsonBudget.Label,
            jsonBudget.Lifetime,
            Lifestyles.Infrastructure.Session.Live.Map.Recurrence.Map(jsonBudget.Recurrence),
            Lifestyles.Infrastructure.Session.Live.Map.Existence.Map(jsonBudget.Existence))
        { }
    }
}
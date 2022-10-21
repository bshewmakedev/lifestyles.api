using Lifestyles.Infrastructure.Session.Budget.Models;
using Lifestyles.Infrastructure.Session.Categorize.Models;
using BudgetMap = Lifestyles.Service.Budget.Map.Budget;

namespace Lifestyles.Infrastructure.Session.Budget.Map
{
    public class Budget : BudgetMap
    {
        public Budget() { }

        public Budget(JsonBudget jsonBudget) : base(
            jsonBudget.Amount ?? 0,
            jsonBudget.Id,
            jsonBudget.Label,
            jsonBudget.Lifetime,
            Lifestyles.Infrastructure.Session.Live.Map.Recurrence.Map(jsonBudget.Recurrence),
            Lifestyles.Infrastructure.Session.Live.Map.Existence.Map(jsonBudget.Existence))
        { }
    }
}
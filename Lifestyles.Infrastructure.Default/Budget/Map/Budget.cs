using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Default.Budget.Models;
using BudgetMap = Lifestyles.Service.Budget.Map.Budget;

namespace Lifestyles.Infrastructure.Default.Budget.Map
{
    public class Budget : BudgetMap
    {
        public Budget(
            IKeyValueRepo context,
            JsonBudget jsonBudget) : base(
            jsonBudget.Amount,
            null,
            jsonBudget.Label,
            jsonBudget.Lifetime,
            Lifestyles.Service.Live.Map.Recurrence.Map(jsonBudget.RecurrenceAlias),
            Lifestyles.Service.Live.Map.Existence.Map(jsonBudget.ExistenceAlias))
        { }
    }
}
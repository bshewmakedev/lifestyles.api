using Lifestyles.Domain.Live.Repositories;
using BudgetMap = Lifestyles.Service.Budget.Map.Budget;
using Lifestyles.Infrastructure.Session.Budget.Models;

namespace Lifestyles.Infrastructure.Session.Budget.Map
{
    public class Budget : BudgetMap
    {
        public Budget(
            IKeyValueRepo context,
            DbBudget dbBudget) : base(
            dbBudget.Amount.HasValue ? dbBudget.Amount.Value : default(decimal),
            dbBudget.Id,
            dbBudget.Label,
            dbBudget.Lifetime,
            Lifestyles.Infrastructure.Session.Live.Models.DbRecurrence.GetRecurrence(context, dbBudget.RecurrenceId),
            Lifestyles.Infrastructure.Session.Live.Models.DbExistence.GetExistence(context, dbBudget.ExistenceId))
        { }
    }
}
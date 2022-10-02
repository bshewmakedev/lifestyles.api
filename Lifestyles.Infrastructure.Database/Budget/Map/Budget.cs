using Lifestyles.Domain.Live.Constants;
using BudgetMap = Lifestyles.Service.Budget.Map.Budget;
using Lifestyles.Infrastructure.Database.Budget.Models;
using Lifestyles.Infrastructure.Database.Live.Models;
using Lifestyles.Infrastructure.Database.Live.Extensions;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Budget.Map
{
    public class Budget : BudgetMap
    {
        public Budget(
            IKeyValueStorage context,
            DbBudget dbBudget) : base(
            dbBudget.Amount.HasValue ? dbBudget.Amount.Value : default(decimal),
            dbBudget.Id,
            dbBudget.Label,
            dbBudget.Lifetime,
            GetRecurrence(context, dbBudget.RecurrenceId),
            GetExistence(context, dbBudget.ExistenceId))
        { }

        public static Recurrence GetRecurrence(
            IKeyValueStorage context,
            Guid? recurrenceId)
        {
            var dbRecurrences = context.GetItem<DataTable>("tbl_Recurrence").GetRows().Select(r => new DbRecurrence(r));

            return Lifestyles.Domain.Live.Map.Recurrence.Map(dbRecurrences.FirstOrDefault(r => r.Id.Equals(recurrenceId))?.Alias);
        }

        public static Existence GetExistence(
            IKeyValueStorage context,
            Guid? existenceId)
        {
            var dbExistences = context.GetItem<DataTable>("tbl_Existence").GetRows().Select(r => new DbExistence(r));

            return Lifestyles.Domain.Live.Map.Existence.Map(dbExistences.FirstOrDefault(r => r.Id.Equals(existenceId))?.Alias);
        }
    }
}
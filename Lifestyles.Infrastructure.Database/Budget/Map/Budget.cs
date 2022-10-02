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
            GetRecurrence(context, dbBudget.RecurrenceId),
            GetExistence(context, dbBudget.ExistenceId))
        { }

        private static Guid GetId(object id)
        {
            var idStr = id.ToString() ?? "";

            return string.IsNullOrWhiteSpace(idStr)
                ? Guid.NewGuid()
                : Guid.Parse(idStr);
        }

        private static decimal GetAmount(object amount)
        {
            return decimal.TryParse(amount.ToString() ?? "", out var amountParsed)
                ? amountParsed
                : default(decimal);
        }

        private static string GetLabel(object label)
        {
            return label.ToString() ?? "";
        }

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
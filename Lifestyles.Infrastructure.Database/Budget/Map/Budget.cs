using Lifestyles.Domain.Measure.Constants;
using BudgetMap = Lifestyles.Service.Budget.Map.Budget;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Budget.Map
{
    public class Budget : BudgetMap
    {
        public Budget(DataRow row) : base(
            GetAmount(row["Amount"]),
            GetId(row["Id"]),
            GetLabel(row["Label"]),
            GetRecurrence(row["RecurrenceAlias"]),
            GetExistence(row["ExistenceAlias"]))
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

        public static Recurrence GetRecurrence(object alias)
        {
            const string RecurrenceNever = "never";
            const string RecurrenceDaily = "daily";
            const string RecurrenceWeekly = "weekly";
            const string RecurrenceMonthly = "monthly";
            const string RecurrenceAnnually = "annually";

            switch (alias.ToString() ?? "")
            {
                case RecurrenceDaily: return Recurrence.Daily;
                case RecurrenceWeekly: return Recurrence.Weekly;
                case RecurrenceMonthly: return Recurrence.Monthly;
                case RecurrenceAnnually: return Recurrence.Annually;
                default: return Recurrence.Never;
            }
        }

        public static Existence GetExistence(object alias)
        {
            const string ExistenceExcluded = "excluded";
            const string ExistenceExpected = "expected";
            const string ExistenceSuggested = "suggested";

            switch (alias.ToString() ?? "")
            {
                case ExistenceExpected: return Existence.Expected;
                case ExistenceSuggested: return Existence.Suggested;
                default: return Existence.Excluded;
            }
        }
    }
}
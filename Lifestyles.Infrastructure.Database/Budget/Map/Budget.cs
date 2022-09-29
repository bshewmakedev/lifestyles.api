using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Measure.Constants;
using BudgetEntity = Lifestyles.Service.Budget.Map.Budget;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Budget.Map
{
    public class Budget : BudgetEntity
    {
        private const string RecurrenceNever = "never";
        private const string RecurrenceDaily = "daily";
        private const string RecurrenceWeekly = "weekly";
        private const string RecurrenceMonthly = "monthly";
        private const string RecurrenceAnnually = "annually";

        public Budget(DataRow row) : base(
            GetAmount(row["Amount"]),
            GetId(row["Id"]),
            GetLabel(row["Label"]),
            GetRecurrence(row["RecurrenceAlias"]))
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
            switch (alias.ToString() ?? "")
            {
                case RecurrenceDaily: return Recurrence.Daily;
                case RecurrenceWeekly: return Recurrence.Weekly;
                case RecurrenceMonthly: return Recurrence.Monthly;
                case RecurrenceAnnually: return Recurrence.Annually;
                default: return Recurrence.Never;
            }
        }
    }
}
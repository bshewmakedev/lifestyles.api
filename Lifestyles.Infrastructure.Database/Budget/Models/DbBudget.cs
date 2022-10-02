using Lifestyles.Infrastructure.Database.Categorize.Models;
using Lifestyles.Infrastructure.Database.Live.Models;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Budget.Models
{
    public class DbBudget : DbLifestyle
    {
        public decimal? Amount { get; set; }

        public DbBudget() : base() { }

        public DbBudget(DataRow row) : base(row)
        {
            Amount = decimal.TryParse(row["Amount"].ToString() ?? "", out var amountParsed)
                ? amountParsed
                : null;
        }

        public static new DataRow AddDataRow(
            DataTable tableBudget,
            Dictionary<string, Guid> budgetTypeIds,
            DbBudget dbBudget)
        {
            DataRow budgetRow = tableBudget.NewRow();
            budgetRow["Id"] = dbBudget.Id;
            budgetRow["BudgetTypeId"] = budgetTypeIds["budget"];
            budgetRow["Amount"] = dbBudget.Amount;
            budgetRow["Label"] = dbBudget.Label;
            budgetRow["Lifetime"] = dbBudget.Lifetime.HasValue ? dbBudget.Lifetime : DBNull.Value;
            budgetRow["RecurrenceId"] = dbBudget.RecurrenceId;
            budgetRow["ExistenceId"] = dbBudget.ExistenceId;
            tableBudget.Rows.Add(budgetRow);

            return budgetRow;
        }
    }
}
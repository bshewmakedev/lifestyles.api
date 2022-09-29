using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Domain.Measure.Constants;
using BudgetMap = Lifestyles.Infrastructure.Database.Budget.Map.Budget;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Budget.Repositories
{
    public class BudgetRepo : IRepository<IBudget>
    {
        private static DataTable GetTestDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(Guid));
            table.Columns.Add("Amount", typeof(decimal));
            table.Columns.Add("Label", typeof(string));
            table.Columns.Add("RecurrenceAlias", typeof(string));

            for (var i = 0; i < 4; i++)
            {
                DataRow row = table.NewRow();
                row["Id"] = Guid.NewGuid();
                row["Amount"] = (10 + i) * Math.Pow(-1, i);
                row["Label"] = $"Budget {i}";
                row["RecurrenceAlias"] = new string[] { "never", "daily", "never", "weekly" }[i];
                table.Rows.Add(row);
            }

            return table;
        }

        public IEnumerable<IBudget> Find(Func<IBudget, bool>? predicate = null)
        {
            var table = GetTestDataTable();

            var budgetsDb = new List<IBudget>();
            foreach (DataRow row in table.Rows)
            {
                budgetsDb.Add(new BudgetMap(row));
            }

            return budgetsDb.Where(predicate ?? ((b) => true));
        }

        public IEnumerable<IBudget> Upsert(IEnumerable<IBudget> budgets)
        {
            return budgets;
        }

        public IEnumerable<IBudget> Remove(IEnumerable<IBudget> budgets)
        {
            var table = GetTestDataTable();

            var budgetsDb = new List<IBudget>();
            foreach (DataRow row in table.Rows)
            {
                budgetsDb.Add(new BudgetMap(row));
            }

            return budgetsDb.Where(b => budgets.All(b2 => !b.Id.Equals(b2.Id)));
        }
    }
}
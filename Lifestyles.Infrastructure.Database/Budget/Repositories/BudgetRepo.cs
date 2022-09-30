using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Repositories;
using BudgetMap = Lifestyles.Infrastructure.Database.Budget.Map.Budget;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Budget.Repositories
{
    public class BudgetRepo : IRepository<IBudget>
    {
        private readonly IKeyValueStorage _context;

        public BudgetRepo(IKeyValueStorage context)
        {
            _context = context;
        }

        public static IEnumerable<DataRow> GetRows(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                yield return row;
            }
        }

        public IEnumerable<IBudget> Find(Func<IBudget, bool>? predicate = null)
        {
            var budgetsDb = new List<IBudget>();
            var tableBudget = _context.GetItem<DataTable>("tbl_Budget");
            var recurrenceRows = GetRows(_context.GetItem<DataTable>("tbl_Recurrence"));
            var existenceRows = GetRows(_context.GetItem<DataTable>("tbl_Existence"));
            foreach (DataRow row in GetRows(tableBudget).Select(br =>
            {
                DataTable table = new DataTable();
                table.Columns.Add("Id", typeof(Guid));
                table.Columns.Add("Amount", typeof(decimal));
                table.Columns.Add("Label", typeof(string));
                table.Columns.Add("RecurrenceAlias", typeof(string));
                table.Columns.Add("ExistenceAlias", typeof(string));

                DataRow row = table.NewRow();
                row["Id"] = br["Id"];
                row["Amount"] = br["Amount"];
                row["Label"] = br["Label"];
                row["RecurrenceAlias"] = recurrenceRows.FirstOrDefault(r => r["Id"].Equals(br["RecurrenceId"]))?["Alias"];
                row["ExistenceAlias"] = existenceRows.FirstOrDefault(r => r["Id"].Equals(br["ExistenceId"]))?["Alias"];

                return row;
            }))
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
            var budgetsDb = new List<IBudget>();
            var tableBudget = _context.GetItem<DataTable>("tbl_Budget");
            var recurrenceRows = GetRows(_context.GetItem<DataTable>("tbl_Recurrence"));
            foreach (DataRow row in GetRows(tableBudget).Select(br =>
            {
                DataTable table = new DataTable();
                table.Columns.Add("Id", typeof(Guid));
                table.Columns.Add("Amount", typeof(decimal));
                table.Columns.Add("Label", typeof(string));
                table.Columns.Add("RecurrenceAlias", typeof(string));
                table.Columns.Add("ExistenceAlias", typeof(string));

                DataRow row = table.NewRow();
                row["Id"] = br["Id"];
                row["Amount"] = br["Amount"];
                row["Label"] = br["Label"];
                row["RecurrenceAlias"] = recurrenceRows.FirstOrDefault(r => r["Id"].Equals(br["Id"]))?["Alias"];
                row["ExistenceAlias"] = recurrenceRows.FirstOrDefault(r => r["Id"].Equals(br["ExistenceId"]))?["Alias"];

                return row;
            }))
            {
                budgetsDb.Add(new BudgetMap(row));
            }

            return budgetsDb.Where(b => budgets.All(b2 => !b.Id.Equals(b2.Id)));
        }
    }
}
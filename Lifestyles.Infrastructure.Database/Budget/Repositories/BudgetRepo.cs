using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;
using BudgetMap = Lifestyles.Infrastructure.Database.Budget.Map.Budget;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Budget.Repositories
{
    public class BudgetRepo : IBudgetRepo
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
            var budgetTable = _context.GetItem<DataTable>("tbl_Budget");
            budgetTable.Columns.Add("RecurrenceAlias", typeof(string));
            budgetTable.Columns.Add("ExistenceAlias", typeof(string));
            var budgetRows = GetRows(budgetTable);
            var budgetTypeRows = GetRows(_context.GetItem<DataTable>("tbl_BudgetType"));
            var recurrenceRows = GetRows(_context.GetItem<DataTable>("tbl_Recurrence"));
            var existenceRows = GetRows(_context.GetItem<DataTable>("tbl_Existence"));
            foreach (DataRow row in budgetRows
                .Where(br =>
                {
                    return br["BudgetTypeId"].ToString()
                        .Equals(budgetTypeRows.FirstOrDefault(btr => btr["Alias"].Equals("budget"))?["Id"]);
                })
                .Select(br =>
                {
                    br["RecurrenceAlias"] = recurrenceRows.FirstOrDefault(r => r["Id"].Equals(br["RecurrenceId"]))?["Alias"];
                    br["ExistenceAlias"] = existenceRows.FirstOrDefault(r => r["Id"].Equals(br["ExistenceId"]))?["Alias"];

                    return br;
                }))
            {
                budgetsDb.Add(new BudgetMap(row));
            }

            return budgetsDb.Where(predicate ?? ((b) => true));
        }

        public IEnumerable<IBudget> FindCategorizedAs(Guid categoryId)
        {
            var categoryRows = GetRows(_context.GetItem<DataTable>("tbl_Category"))
                .Where(r => (r["CategoryId"]?.ToString() ?? "").Equals(categoryId.ToString()));

            var budgetsDb = new List<IBudget>();
            var budgetTable = _context.GetItem<DataTable>("tbl_Budget");
            budgetTable.Columns.Add("RecurrenceAlias", typeof(string));
            budgetTable.Columns.Add("ExistenceAlias", typeof(string));
            var budgetRows = GetRows(budgetTable);
            var budgetTypeRows = GetRows(_context.GetItem<DataTable>("tbl_BudgetType"));
            var recurrenceRows = GetRows(_context.GetItem<DataTable>("tbl_Recurrence"));
            var existenceRows = GetRows(_context.GetItem<DataTable>("tbl_Existence"));
            foreach (DataRow row in budgetRows.Join(
                categoryRows, 
                br => br["Id"], 
                cr => cr["BudgetId"], (br, cr) => br)
                .Where(br =>
                {
                    return br["BudgetTypeId"].ToString()
                        .Equals(budgetTypeRows.FirstOrDefault(btr => btr["Alias"].Equals("budget"))?["Id"]);
                })
                .Select(br =>
                {
                    br["RecurrenceAlias"] = recurrenceRows.FirstOrDefault(r => r["Id"].Equals(br["RecurrenceId"]))?["Alias"];
                    br["ExistenceAlias"] = existenceRows.FirstOrDefault(r => r["Id"].Equals(br["ExistenceId"]))?["Alias"];

                    return br;
                }))
            {
                budgetsDb.Add(new BudgetMap(row));
            }
            
            return budgetsDb;
        }

        public IEnumerable<IBudget> Upsert(IEnumerable<IBudget> budgets)
        {
            return budgets;
        }

        public IEnumerable<IBudget> Remove(IEnumerable<IBudget> budgets)
        {
            var budgetsDb = Find();

            return budgetsDb.Where(b => budgets.All(b2 => !b.Id.Equals(b2.Id)));
        }
    }
}
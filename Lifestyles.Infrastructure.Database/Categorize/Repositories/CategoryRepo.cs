using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Categorize.Repositories;
using BudgetMap = Lifestyles.Infrastructure.Database.Budget.Map.Budget;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Categorize.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly IKeyValueStorage _context;

        public CategoryRepo(IKeyValueStorage context)
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

        public IEnumerable<ICategory> Find(Func<ICategory, bool>? predicate = null)
        {
            var budgetsDb = new List<ICategory>();
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
                        .Equals(budgetTypeRows.FirstOrDefault(btr => btr["Alias"].Equals("category"))?["Id"]);
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

        public IEnumerable<ICategory> Upsert(IEnumerable<ICategory> budgets)
        {
            return budgets;
        }

        public IEnumerable<ICategory> Remove(IEnumerable<ICategory> budgets)
        {
            var budgetsDb = new List<ICategory>();
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
                        .Equals(budgetTypeRows.FirstOrDefault(btr => btr["Alias"].Equals("category"))?["Id"]);
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

            return budgetsDb.Where(b => budgets.All(b2 => !b.Id.Equals(b2.Id)));
        }
    }
}
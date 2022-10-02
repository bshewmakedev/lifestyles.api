using Lifestyles.Infrastructure.Database.Budget.Models;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Categorize.Models
{
    public class DbCategory
    {
        public Guid Id { get; set; }
        public Guid BudgetTypeId { get; set; }
        public string Label { get; set; }

        public DbCategory() { }

        public DbCategory(DataRow row)
        {
            Id = Guid.Parse(row["Id"].ToString() ?? "");
            BudgetTypeId = Guid.Parse(row["BudgetTypeId"].ToString() ?? "");
            Label = row["Label"].ToString() ?? "";
        }

        public static DataTable CreateDataTable(IKeyValueStorage keyValueStorage)
        {
            var tableBudget = keyValueStorage.GetItem<DataTable>("tbl_Budget");
            if (tableBudget == null)
            {
                tableBudget = new DataTable();
                tableBudget.Columns.Add("Id", typeof(Guid));
                tableBudget.Columns.Add("BudgetTypeId", typeof(Guid));
                tableBudget.Columns.Add(new DataColumn { ColumnName = "Amount", DataType = typeof(decimal), AllowDBNull = true });
                tableBudget.Columns.Add("Label", typeof(string));
                tableBudget.Columns.Add(new DataColumn { ColumnName = "Lifetime", DataType = typeof(decimal), AllowDBNull = true });
                tableBudget.Columns.Add(new DataColumn { ColumnName = "RecurrenceId", DataType = typeof(Guid), AllowDBNull = true });
                tableBudget.Columns.Add(new DataColumn { ColumnName = "ExistenceId", DataType = typeof(Guid), AllowDBNull = true });
            }

            return tableBudget;
        }

        public static DataRow AddDataRow(
            DataTable tableBudget,
            Dictionary<string, Guid> budgetTypeIds,
            DbCategory dbCategory)
        {
            DataRow categoryRow = tableBudget.NewRow();
            categoryRow["Id"] = dbCategory.Id;
            categoryRow["BudgetTypeId"] = budgetTypeIds["category"];
            categoryRow["Amount"] = DBNull.Value;
            categoryRow["Label"] = dbCategory.Label;
            categoryRow["Lifetime"] = DBNull.Value;
            categoryRow["RecurrenceId"] = DBNull.Value;
            categoryRow["ExistenceId"] = DBNull.Value;
            tableBudget.Rows.Add(categoryRow);

            return categoryRow;
        }
    }
}
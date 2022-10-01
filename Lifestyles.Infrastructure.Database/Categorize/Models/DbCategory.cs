using System.Data;

namespace Lifestyles.Infrastructure.Database.Categorize.Models
{
    public class DbCategory
    {
        public Guid Id { get; set; }
        public Guid BudgetId { get; set; }
        public Guid CategoryId { get; set; }

        public static Dictionary<string, Guid> Default(
            IKeyValueStorage keyValueStorage,
            Dictionary<string, Guid> budgetTypeIds,
            Dictionary<string, Guid> lifestyleIds)
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
            var tableCategory = keyValueStorage.GetItem<DataTable>("tbl_Category");
            if (tableCategory == null)
            {
                tableCategory = new DataTable();
                tableCategory.Columns.Add("Id", typeof(Guid));
                tableCategory.Columns.Add("BudgetId", typeof(Guid));
                tableCategory.Columns.Add("CategoryId", typeof(Guid));
            }

            var categoryIds = new Dictionary<string, Guid>();
            foreach (var category in new[] { "connect", "eat & hydrate", "hike", "shelter", "wear" })
            {
                var id = Guid.NewGuid();
                DataRow categoryRow = tableBudget.NewRow();
                categoryRow["Id"] = id;
                categoryRow["BudgetTypeId"] = budgetTypeIds["category"];
                categoryRow["Amount"] = DBNull.Value;
                categoryRow["Label"] = category;
                categoryRow["Lifetime"] = DBNull.Value;
                categoryRow["RecurrenceId"] = DBNull.Value;
                categoryRow["ExistenceId"] = DBNull.Value;
                tableBudget.Rows.Add(categoryRow);
                categoryIds.Add(category, id);

                foreach (var lifestyleId in lifestyleIds)
                {
                    DataRow categorizedAsRow = tableCategory.NewRow();
                    categorizedAsRow["Id"] = Guid.NewGuid();
                    categorizedAsRow["BudgetId"] = id;
                    categorizedAsRow["CategoryId"] = lifestyleId.Value;
                    tableCategory.Rows.Add(categorizedAsRow);
                }
            }
            keyValueStorage.SetItem("tbl_Budget", tableBudget);
            keyValueStorage.SetItem("tbl_Category", tableCategory);
            
            return categoryIds;
        }
    }
}
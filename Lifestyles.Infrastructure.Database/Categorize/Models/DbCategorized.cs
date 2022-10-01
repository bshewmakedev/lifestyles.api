using System.Data;

namespace Lifestyles.Infrastructure.Database.Categorize.Models
{
    public class DbCategorized
    {
        public Guid Id { get; set; }
        public Guid BudgetId { get; set; }
        public Guid CategoryId { get; set; }

        public static DataTable CreateDataTable(IKeyValueStorage keyValueStorage)
        {
            var tableCategory = keyValueStorage.GetItem<DataTable>("tbl_Categorized");
            if (tableCategory == null)
            {
                tableCategory = new DataTable();
                tableCategory.Columns.Add("Id", typeof(Guid));
                tableCategory.Columns.Add("BudgetId", typeof(Guid));
                tableCategory.Columns.Add("CategoryId", typeof(Guid));
            }

            return tableCategory;
        }

        public static DataRow AddDataRow(
            DataTable tableCategorized,
            DbCategorized dbCategorized)
        {
            DataRow categorizedRow = tableCategorized.NewRow();
            categorizedRow["Id"] = dbCategorized.Id;
            categorizedRow["BudgetId"] = dbCategorized.BudgetId;
            categorizedRow["CategoryId"] = dbCategorized.BudgetId;
            tableCategorized.Rows.Add(categorizedRow);

            return categorizedRow;
        }
    }
}
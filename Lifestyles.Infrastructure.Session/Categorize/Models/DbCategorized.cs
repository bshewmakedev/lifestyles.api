using Lifestyles.Domain.Live.Repositories;
using System.Data;

namespace Lifestyles.Infrastructure.Session.Categorize.Models
{
    public class DbCategorized
    {
        public Guid Id { get; set; }
        public Guid BudgetId { get; set; }
        public Guid CategoryId { get; set; }

        public static DataTable CreateDataTable(IKeyValueRepo keyValueStorage)
        {
            var tableCategorized = keyValueStorage.GetItem<DataTable>("tbl_Categorized");
            if (tableCategorized == null)
            {
                tableCategorized = new DataTable();
                tableCategorized.Columns.Add("Id", typeof(Guid));
                tableCategorized.Columns.Add("BudgetId", typeof(Guid));
                tableCategorized.Columns.Add("CategoryId", typeof(Guid));
            }

            return tableCategorized;
        }

        public static DataRow AddDataRow(
            DataTable tableCategorized,
            DbCategorized dbCategorized)
        {
            DataRow categorizedRow = tableCategorized.NewRow();
            categorizedRow["Id"] = dbCategorized.Id;
            categorizedRow["BudgetId"] = dbCategorized.BudgetId;
            categorizedRow["CategoryId"] = dbCategorized.CategoryId;
            tableCategorized.Rows.Add(categorizedRow);

            return categorizedRow;
        }
    }
}
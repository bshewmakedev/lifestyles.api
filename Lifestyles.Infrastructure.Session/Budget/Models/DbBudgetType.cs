using Lifestyles.Domain.Live.Repositories;
using System.Data;

namespace Lifestyles.Infrastructure.Session.Budget.Models
{
    public class DbBudgetType
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }

        public DbBudgetType() { }

        public DbBudgetType(DataRow row)
        {
            Id = Guid.Parse(row["Id"].ToString() ?? "");
            Alias = row["Alias"].ToString() ?? "";
        }

        public static DataTable CreateDataTable(IKeyValueRepo keyValueStorage)
        {
            var tableBudgetType = keyValueStorage.GetItem<DataTable>("tbl_BudgetType");
            if (tableBudgetType == null)
            {
                tableBudgetType = new DataTable();
                tableBudgetType.Columns.Add("Id", typeof(Guid));
                tableBudgetType.Columns.Add("Alias", typeof(string));
            }

            return tableBudgetType;
        }

        public static DataRow AddDataRow(
            DataTable table,
            DbBudgetType dbData)
        {
            var row = table.NewRow(); 
            row["Id"] = dbData.Id;
            row["Alias"] = dbData.Alias;
            table.Rows.Add(row);

            return row;
        }
    }
}
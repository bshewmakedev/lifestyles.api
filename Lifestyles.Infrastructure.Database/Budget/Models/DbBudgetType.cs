using System.Data;

namespace Lifestyles.Infrastructure.Database.Budget.Models
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

        public static DataTable CreateDataTable(IKeyValueStorage keyValueStorage)
        {
            var tableBudgetType = new DataTable();
            tableBudgetType.Columns.Add("Id", typeof(Guid));
            tableBudgetType.Columns.Add("Alias", typeof(string));

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

        public static Dictionary<string, Guid> Default(IKeyValueStorage keyValueStorage)
        {
            var tableBudgetType = CreateDataTable(keyValueStorage);
            var budgetTypeIds = new Dictionary<string, Guid>();
            foreach (var dbBudgetType in new DbBudgetType[] {
                new DbBudgetType { Id = Guid.NewGuid(), Alias = "budget" },
                new DbBudgetType { Id = Guid.NewGuid(), Alias = "category" },
                new DbBudgetType { Id = Guid.NewGuid(), Alias = "lifestyle" }
            })
            {
                AddDataRow(tableBudgetType, dbBudgetType);
                budgetTypeIds.Add(dbBudgetType.Alias, dbBudgetType.Id);
            }
            keyValueStorage.SetItem("tbl_BudgetType", tableBudgetType);

            return budgetTypeIds;
        }
    }
}
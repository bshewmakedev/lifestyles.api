using System.Data;

namespace Lifestyles.Infrastructure.Database.Live.Models
{
    public class DbExistence
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }

        public DbExistence() { }

        public DbExistence(DataRow row)
        {
            Id = Guid.Parse(row["Id"].ToString() ?? "");
            Alias = row["Alias"].ToString() ?? "";
        }

        public static DataTable CreateDataTable(IKeyValueStorage keyValueStorage)
        {
            var tableExistence = new DataTable();
            tableExistence.Columns.Add("Id", typeof(Guid));
            tableExistence.Columns.Add("Alias", typeof(string));

            return tableExistence;
        }

        public static DataRow AddDataRow(
            DataTable table,
            DbExistence dbData)
        {
            var row = table.NewRow(); 
            row["Id"] = dbData.Id;
            row["Alias"] = dbData.Alias;
            table.Rows.Add(row);

            return row;
        }

        public static Dictionary<string, Guid> Default(IKeyValueStorage keyValueStorage)
        {
            var tableExistence = CreateDataTable(keyValueStorage);
            var existenceIds = new Dictionary<string, Guid>();
            foreach (var dbExistence in new DbExistence[] {
                new DbExistence { Id = Guid.NewGuid(), Alias = "excluded" },
                new DbExistence { Id = Guid.NewGuid(), Alias = "expected" },
                new DbExistence { Id = Guid.NewGuid(), Alias = "suggested" }
            })
            {
                AddDataRow(tableExistence, dbExistence);
                existenceIds.Add(dbExistence.Alias, dbExistence.Id);
            }
            keyValueStorage.SetItem("tbl_Existence", tableExistence);

            return existenceIds;
        }
    }
}
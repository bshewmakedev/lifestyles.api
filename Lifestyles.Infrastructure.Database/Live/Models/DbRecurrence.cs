using System.Data;

namespace Lifestyles.Infrastructure.Database.Live.Models
{
    public class DbRecurrence
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }

        public DbRecurrence() { }

        public DbRecurrence(DataRow row)
        {
            Id = Guid.Parse(row["Id"].ToString() ?? "");
            Alias = row["Alias"].ToString() ?? "";
        }

        public static DataTable CreateDataTable(IKeyValueStorage keyValueStorage)
        {
            var tableRecurrence = new DataTable();
            tableRecurrence.Columns.Add("Id", typeof(Guid));
            tableRecurrence.Columns.Add("Alias", typeof(string));

            return tableRecurrence;
        }

        public static DataRow AddDataRow(
            DataTable table,
            DbRecurrence dbData)
        {
            var row = table.NewRow(); 
            row["Id"] = dbData.Id;
            row["Alias"] = dbData.Alias;
            table.Rows.Add(row);

            return row;
        }

        public static Dictionary<string, Guid> Default(IKeyValueStorage keyValueStorage)
        {
            var tableRecurrence = CreateDataTable(keyValueStorage);
            var recurrenceIds = new Dictionary<string, Guid>();
            foreach (var dbRecurrence in new DbRecurrence[] {
                new DbRecurrence { Id = Guid.NewGuid(), Alias = "never" },
                new DbRecurrence { Id = Guid.NewGuid(), Alias = "daily" },
                new DbRecurrence { Id = Guid.NewGuid(), Alias = "weekly" },
                new DbRecurrence { Id = Guid.NewGuid(), Alias = "monthly" },
                new DbRecurrence { Id = Guid.NewGuid(), Alias = "annually" }
            })
            {
                AddDataRow(tableRecurrence, dbRecurrence);
                recurrenceIds.Add(dbRecurrence.Alias, dbRecurrence.Id);
            }
            keyValueStorage.SetItem("tbl_Recurrence", tableRecurrence);

            return recurrenceIds;
        }
    }
}
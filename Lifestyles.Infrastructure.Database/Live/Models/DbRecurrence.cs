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
            var tableRecurrence = keyValueStorage.GetItem<DataTable>("tbl_Recurrence");
            if (tableRecurrence == null)
            {
                tableRecurrence = new DataTable();
                tableRecurrence.Columns.Add("Id", typeof(Guid));
                tableRecurrence.Columns.Add("Alias", typeof(string));
            }

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
    }
}
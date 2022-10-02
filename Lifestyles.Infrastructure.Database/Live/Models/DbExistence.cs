using Lifestyles.Domain.Live.Repositories;
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

        public static DataTable CreateDataTable(IKeyValueRepo keyValueStorage)
        {
            var tableExistence = keyValueStorage.GetItem<DataTable>("tbl_Existence");
            if (tableExistence == null)
            {
                tableExistence = new DataTable();
                tableExistence.Columns.Add("Id", typeof(Guid));
                tableExistence.Columns.Add("Alias", typeof(string));
            }

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
    }
}
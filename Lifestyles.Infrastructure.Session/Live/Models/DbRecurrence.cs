using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Session.Live.Extensions;
using System.Data;

namespace Lifestyles.Infrastructure.Session.Live.Models
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

        public static DataTable CreateDataTable(IKeyValueRepo keyValueStorage)
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

        public static Lifestyles.Domain.Live.Entities.Recurrence GetRecurrence(
            IKeyValueRepo context,
            Guid? recurrenceId)
        {
            var dbRecurrences = context.GetItem<DataTable>("tbl_Recurrence").GetRows().Select(r => new DbRecurrence(r));

            return Lifestyles.Service.Live.Map.Recurrence.Map(dbRecurrences.FirstOrDefault(r => r.Id.Equals(recurrenceId))?.Alias);
        }
    }
}
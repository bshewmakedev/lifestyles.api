using System.Data;

namespace Lifestyles.Infrastructure.Database.Measure.Models
{
    public class DbRecurrence
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }

        public static Dictionary<string, Guid> Default(IKeyValueStorage keyValueStorage)
        {
            var tableRecurrence = new DataTable();
            
            tableRecurrence.Columns.Add("Id", typeof(Guid));
            tableRecurrence.Columns.Add("Alias", typeof(string));
            var recurrenceIds = new Dictionary<string, Guid>();
            foreach (var recurrence in new[] { "never", "daily", "weekly", "monthly", "annually" })
            {
                var id = Guid.NewGuid();
                var dataRow = tableRecurrence.NewRow();
                dataRow["Id"] = id;
                dataRow["Alias"] = recurrence;
                tableRecurrence.Rows.Add(dataRow);
                recurrenceIds.Add(recurrence, id);
            }
            keyValueStorage.SetItem("tbl_Recurrence", tableRecurrence);

            return recurrenceIds;
        }
    }
}
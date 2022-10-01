using System.Data;

namespace Lifestyles.Infrastructure.Database.Measure.Models
{
    public class DbExistence
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }

        public static Dictionary<string, Guid> Default(IKeyValueStorage keyValueStorage)
        {
            var tableExistence = new DataTable();

            tableExistence.Columns.Add("Id", typeof(Guid));
            tableExistence.Columns.Add("Alias", typeof(string));
            var existenceIds = new Dictionary<string, Guid>();
            foreach (var existence in new[] { "excluded", "expected", "suggested" })
            {
                var id = Guid.NewGuid();
                var dataRow = tableExistence.NewRow();
                dataRow["Id"] = id;
                dataRow["Alias"] = existence;
                tableExistence.Rows.Add(dataRow);
                existenceIds.Add(existence, id);
            }
            keyValueStorage.SetItem("tbl_Existence", tableExistence);

            return existenceIds;
        }
    }
}
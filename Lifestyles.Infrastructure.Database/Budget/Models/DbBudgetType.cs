using System.Data;

namespace Lifestyles.Infrastructure.Database.Budget.Models
{
    public class DbBudgetType
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }

        public static Dictionary<string, Guid> Default(IKeyValueStorage keyValueStorage)
        {
            var tableBudgetType = new DataTable();

            tableBudgetType.Columns.Add("Id", typeof(Guid));
            tableBudgetType.Columns.Add("Alias", typeof(string));
            var budgetTypeIds = new Dictionary<string, Guid>();
            foreach (var existence in new[] { "budget", "category", "lifestyle" })
            {
                var id = Guid.NewGuid();
                var dataRow = tableBudgetType.NewRow();
                dataRow["Id"] = id;
                dataRow["Alias"] = existence;
                tableBudgetType.Rows.Add(dataRow);
                budgetTypeIds.Add(existence, id);
            }
            keyValueStorage.SetItem("tbl_BudgetType", tableBudgetType);

            return budgetTypeIds;
        }
    }
}
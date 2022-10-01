using Lifestyles.Infrastructure.Database.Budget.Models;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Live.Models
{
    public class DbLifestyle : DbBudget
    {
        public static Dictionary<string, Guid> Default(
            IKeyValueStorage keyValueStorage,
            Dictionary<string, Guid> budgetTypeIds)
        {
            var tableBudget = keyValueStorage.GetItem<DataTable>("tbl_Budget");
            if (tableBudget == null)
            {
                tableBudget = new DataTable();
                tableBudget.Columns.Add("Id", typeof(Guid));
                tableBudget.Columns.Add("BudgetTypeId", typeof(Guid));
                tableBudget.Columns.Add(new DataColumn { ColumnName = "Amount", DataType = typeof(decimal), AllowDBNull = true });
                tableBudget.Columns.Add("Label", typeof(string));
                tableBudget.Columns.Add(new DataColumn { ColumnName = "Lifetime", DataType = typeof(decimal), AllowDBNull = true });
                tableBudget.Columns.Add(new DataColumn { ColumnName = "RecurrenceId", DataType = typeof(Guid), AllowDBNull = true });
                tableBudget.Columns.Add(new DataColumn { ColumnName = "ExistenceId", DataType = typeof(Guid), AllowDBNull = true });
            }
           
            var AddLifestyle = (DbLifestyle lifestyle) =>
            {
                DataRow budgetRow = tableBudget.NewRow();
                budgetRow["Id"] = lifestyle.Id;
                budgetRow["BudgetTypeId"] = budgetTypeIds["lifestyle"];
                budgetRow["Amount"] = DBNull.Value;
                budgetRow["Label"] = lifestyle.Label;
                budgetRow["Lifetime"] = DBNull.Value;
                budgetRow["RecurrenceId"] = DBNull.Value;
                budgetRow["ExistenceId"] = DBNull.Value;
                tableBudget.Rows.Add(budgetRow);
            };
            var lifestyleIds = new Dictionary<string, Guid>();
            foreach (var lifestyle in new DbLifestyle[] { 
                new DbLifestyle { Id = Guid.NewGuid(), Label = "Appalachian Trail" } 
            })
            {
                AddLifestyle(lifestyle);
                lifestyleIds.Add(lifestyle.Label, lifestyle.Id);
            }
            keyValueStorage.SetItem("tbl_Budget", tableBudget);

            return lifestyleIds;
        }
    }
}
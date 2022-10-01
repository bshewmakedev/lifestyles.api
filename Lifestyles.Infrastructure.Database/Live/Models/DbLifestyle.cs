using Lifestyles.Infrastructure.Database.Categorize.Models;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Live.Models
{
    public class DbLifestyle : DbCategory
    {
        public decimal? Lifetime { get; set; }
        public Guid? RecurrenceId { get; set; }
        public Guid? ExistenceId { get; set; }

        public DbLifestyle() : base() { }

        public DbLifestyle(DataRow row) : base(row)
        {
            Lifetime = decimal.TryParse(row["Lifetime"].ToString() ?? "", out var lifetimeParsed)
                ? lifetimeParsed
                : null;
            RecurrenceId = Guid.TryParse(row["RecurrenceId"].ToString() ?? "", out var recurrenceIdParsed)
                ? recurrenceIdParsed
                : null;
            ExistenceId = Guid.TryParse(row["ExistenceId"].ToString() ?? "", out var existenceIdParsed)
                ? existenceIdParsed
                : null;
        }

        public static new DataRow AddDataRow(
            DataTable tableBudget,
            Dictionary<string, Guid> budgetTypeIds,
            DbLifestyle dbLifestyle)
        {
            DataRow lifestyleRow = tableBudget.NewRow();
            lifestyleRow["Id"] = dbLifestyle.Id;
            lifestyleRow["BudgetTypeId"] = budgetTypeIds["lifestyle"];
            lifestyleRow["Amount"] = DBNull.Value;
            lifestyleRow["Label"] = dbLifestyle.Label;
            lifestyleRow["Lifetime"] = dbLifestyle.Lifetime.HasValue ? dbLifestyle.Lifetime : DBNull.Value;
            lifestyleRow["RecurrenceId"] = dbLifestyle.RecurrenceId;
            lifestyleRow["ExistenceId"] = dbLifestyle.ExistenceId;
            tableBudget.Rows.Add(lifestyleRow);

            return lifestyleRow;
        }

        public static Dictionary<string, Guid> Default(
            IKeyValueStorage keyValueStorage,
            Dictionary<string, Guid> budgetTypeIds,
            Dictionary<string, Guid> recurrenceIds,
            Dictionary<string, Guid> existenceIds)
        {
            var tableBudget = CreateDataTable(keyValueStorage);
            var lifestyleIds = new Dictionary<string, Guid>();
            foreach (var lifestyle in new DbLifestyle[] {
                new DbLifestyle { Id = Guid.NewGuid(), Label = "Appalachian Trail",        Lifetime = 6, RecurrenceId = recurrenceIds["monthly"], ExistenceId = existenceIds["expected"] },
                new DbLifestyle { Id = Guid.NewGuid(), Label = "Pacific Crest Trail",      Lifetime = 5, RecurrenceId = recurrenceIds["monthly"], ExistenceId = existenceIds["expected"] },
                new DbLifestyle { Id = Guid.NewGuid(), Label = "Continental Divide Trail", Lifetime = 6, RecurrenceId = recurrenceIds["monthly"], ExistenceId = existenceIds["expected"] },
            })
            {
                AddDataRow(tableBudget, budgetTypeIds, lifestyle);
                lifestyleIds.Add(lifestyle.Label, lifestyle.Id);
            }
            keyValueStorage.SetItem("tbl_Budget", tableBudget);

            return lifestyleIds;
        }
    }
}
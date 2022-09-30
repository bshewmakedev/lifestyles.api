using Lifestyles.Infrastructure.Database.Budget.Models;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Live.Models
{
    public class AppalachianTrail
    {
        public static void Create(IKeyValueStorage keyValueStorage)
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

            var tableBudget = new DataTable();
            tableBudget.Columns.Add("Id", typeof(Guid));
            tableBudget.Columns.Add(new DataColumn { ColumnName = "Amount", DataType = typeof(decimal), AllowDBNull = true });
            tableBudget.Columns.Add("Label", typeof(string));
            tableBudget.Columns.Add(new DataColumn { ColumnName = "Lifetime", DataType = typeof(decimal), AllowDBNull = true });
            tableBudget.Columns.Add(new DataColumn { ColumnName = "RecurrenceId", DataType = typeof(Guid), AllowDBNull = true });
            tableBudget.Columns.Add(new DataColumn { ColumnName = "ExistenceId", DataType = typeof(Guid), AllowDBNull = true });

            var lifestyleIds = new Dictionary<string, Guid>();
            var lifestyleAtId = Guid.NewGuid();
            DataRow lifestyleRow = tableBudget.NewRow();
            lifestyleRow["Id"] = lifestyleAtId;
            lifestyleRow["Amount"] = DBNull.Value;
            lifestyleRow["Label"] = "Appalachian Trail";
            lifestyleRow["Lifetime"] = DBNull.Value;
            lifestyleRow["RecurrenceId"] = DBNull.Value;
            lifestyleRow["ExistenceId"] = DBNull.Value;
            tableBudget.Rows.Add(lifestyleRow);
            lifestyleIds.Add("Appalachian Trail", lifestyleAtId);

            var tableCategory = new DataTable();
            tableCategory.Columns.Add("Id", typeof(Guid));
            tableCategory.Columns.Add("BudgetId", typeof(Guid));
            tableCategory.Columns.Add("CategoryId", typeof(Guid));

            var categoryIds = new Dictionary<string, Guid>();
            foreach (var category in new[] { "connect", "eat & hydrate", "hike", "shelter", "wear" })
            {
                var id = Guid.NewGuid();
                DataRow categoryRow = tableBudget.NewRow();
                categoryRow["Id"] = id;
                categoryRow["Amount"] = DBNull.Value;
                categoryRow["Label"] = category;
                categoryRow["Lifetime"] = DBNull.Value;
                categoryRow["RecurrenceId"] = DBNull.Value;
                categoryRow["ExistenceId"] = DBNull.Value;
                tableBudget.Rows.Add(categoryRow);
                categoryIds.Add(category, id);

                DataRow categorizedAsRow = tableCategory.NewRow();
                categorizedAsRow["Id"] = Guid.NewGuid();
                categorizedAsRow["BudgetId"] = id;
                categorizedAsRow["CategoryId"] = lifestyleAtId;
                tableCategory.Rows.Add(categorizedAsRow);
            }

            var budgetsEatHydrate = new DbBudget[] {
                new DbBudget { Id = Guid.NewGuid(), Amount = -40,   ExistenceId = existenceIds["expected"],  Label = "water filter",          Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -40,   ExistenceId = existenceIds["expected"],  Label = "groceries",             Lifetime = 4,    RecurrenceId = recurrenceIds["daily"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -50,   ExistenceId = existenceIds["expected"],  Label = "stove",                 Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -50,   ExistenceId = existenceIds["expected"],  Label = "pot",                   Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -10,   ExistenceId = existenceIds["expected"],  Label = "utensil",               Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -7,    ExistenceId = existenceIds["expected"],  Label = "stove fuel",            Lifetime = 2,    RecurrenceId = recurrenceIds["weekly"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -90,   ExistenceId = existenceIds["expected"],  Label = "food storage",          Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -25,   ExistenceId = existenceIds["expected"],  Label = "eating & drinking out", Lifetime = 4,    RecurrenceId = recurrenceIds["daily"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -12,   ExistenceId = existenceIds["expected"],  Label = "trowel",                Lifetime = null, RecurrenceId = recurrenceIds["never"] },
            };
            foreach (var budget in budgetsEatHydrate)
            {
                var id = Guid.NewGuid();
                DataRow budgetRow = tableBudget.NewRow();
                budgetRow["Id"] = id;
                budgetRow["Amount"] = budget.Amount;
                budgetRow["Label"] = budget.Label;
                budgetRow["Lifetime"] = budget.Lifetime.HasValue ? budget.Lifetime.Value : DBNull.Value;
                budgetRow["RecurrenceId"] = budget.RecurrenceId.HasValue ? budget.RecurrenceId.Value : DBNull.Value;
                budgetRow["ExistenceId"] = budget.ExistenceId.HasValue ? budget.ExistenceId.Value : DBNull.Value;
                tableBudget.Rows.Add(budgetRow);

                DataRow categorizedAsRow = tableCategory.NewRow();
                categorizedAsRow["Id"] = Guid.NewGuid();
                categorizedAsRow["BudgetId"] = id;
                categorizedAsRow["CategoryId"] = categoryIds["eat & hydrate"];
                tableCategory.Rows.Add(categorizedAsRow);
            }
            var budgetsConnect = new DbBudget[] {
                new DbBudget { Id = Guid.NewGuid(), Amount = -5,    ExistenceId = existenceIds["expected"],  Label = "whistle",               Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -65,   ExistenceId = existenceIds["expected"],  Label = "mobile service",        Lifetime = 1,    RecurrenceId = recurrenceIds["monthly"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -25,   ExistenceId = existenceIds["expected"],  Label = "power brick",           Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -400,  ExistenceId = existenceIds["expected"],  Label = "tent",                  Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -150,  ExistenceId = existenceIds["expected"],  Label = "sleeping bag",          Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -45,   ExistenceId = existenceIds["expected"],  Label = "sleeping pad",          Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -35,   ExistenceId = existenceIds["expected"],  Label = "town lodging",          Lifetime = 4,    RecurrenceId = recurrenceIds["daily"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -25,   ExistenceId = existenceIds["suggested"], Label = "mobile phone",          Lifetime = 1,    RecurrenceId = recurrenceIds["monthly"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -15,   ExistenceId = existenceIds["suggested"], Label = "wall charger",          Lifetime = null, RecurrenceId = recurrenceIds["never"] },
            };
            foreach (var budget in budgetsConnect)
            {
                var id = Guid.NewGuid();
                DataRow budgetRow = tableBudget.NewRow();
                budgetRow["Id"] = id;
                budgetRow["Amount"] = budget.Amount;
                budgetRow["Label"] = budget.Label;
                budgetRow["Lifetime"] = budget.Lifetime.HasValue ? budget.Lifetime.Value : DBNull.Value;
                budgetRow["RecurrenceId"] = budget.RecurrenceId.HasValue ? budget.RecurrenceId.Value : DBNull.Value;
                budgetRow["ExistenceId"] = budget.ExistenceId.HasValue ? budget.ExistenceId.Value : DBNull.Value;
                tableBudget.Rows.Add(budgetRow);

                DataRow categorizedAsRow = tableCategory.NewRow();
                categorizedAsRow["Id"] = Guid.NewGuid();
                categorizedAsRow["BudgetId"] = id;
                categorizedAsRow["CategoryId"] = categoryIds["connect"];
                tableCategory.Rows.Add(categorizedAsRow);
            }
            var budgetsWear = new DbBudget[] {
                new DbBudget { Id = Guid.NewGuid(), Amount = -90,   ExistenceId = existenceIds["expected"],  Label = "trail shoes",           Lifetime = 2,    RecurrenceId = recurrenceIds["monthly"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -30,   ExistenceId = existenceIds["expected"],  Label = "socks",                 Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -30,   ExistenceId = existenceIds["expected"],  Label = "underwear",             Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -80,   ExistenceId = existenceIds["expected"],  Label = "base layer bottoms",    Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -50,   ExistenceId = existenceIds["expected"],  Label = "bottoms",               Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -90,   ExistenceId = existenceIds["expected"],  Label = "shell bottoms",         Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -80,   ExistenceId = existenceIds["expected"],  Label = "base layer top",        Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -35,   ExistenceId = existenceIds["expected"],  Label = "top",                   Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -225,  ExistenceId = existenceIds["expected"],  Label = "insulated top",         Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -90,   ExistenceId = existenceIds["expected"],  Label = "shell top",             Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -15,   ExistenceId = existenceIds["expected"],  Label = "head net",              Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -15,   ExistenceId = existenceIds["expected"],  Label = "beanie",                Lifetime = null, RecurrenceId = recurrenceIds["never"] },
            };
            foreach (var budget in budgetsWear)
            {
                var id = Guid.NewGuid();
                DataRow budgetRow = tableBudget.NewRow();
                budgetRow["Id"] = id;
                budgetRow["Amount"] = budget.Amount;
                budgetRow["Label"] = budget.Label;
                budgetRow["Lifetime"] = budget.Lifetime.HasValue ? budget.Lifetime.Value : DBNull.Value;
                budgetRow["RecurrenceId"] = budget.RecurrenceId.HasValue ? budget.RecurrenceId.Value : DBNull.Value;
                budgetRow["ExistenceId"] = budget.ExistenceId.HasValue ? budget.ExistenceId.Value : DBNull.Value;
                tableBudget.Rows.Add(budgetRow);

                DataRow categorizedAsRow = tableCategory.NewRow();
                categorizedAsRow["Id"] = Guid.NewGuid();
                categorizedAsRow["BudgetId"] = id;
                categorizedAsRow["CategoryId"] = categoryIds["wear"];
                tableCategory.Rows.Add(categorizedAsRow);
            }
            var budgetsHike = new DbBudget[] {
                new DbBudget { Id = Guid.NewGuid(), Amount = -50,   ExistenceId = existenceIds["expected"],  Label = "headlamp",              Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -75,   ExistenceId = existenceIds["expected"],  Label = "trekking poles",        Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -220,  ExistenceId = existenceIds["expected"],  Label = "backpack",              Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -21,   ExistenceId = existenceIds["expected"],  Label = "compass",               Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -40,   ExistenceId = existenceIds["expected"],  Label = "maps",                  Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = -20,   ExistenceId = existenceIds["expected"],  Label = "first aid kit",         Lifetime = 3,    RecurrenceId = recurrenceIds["monthly"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = 0,     ExistenceId = existenceIds["suggested"], Label = "health insurance",      Lifetime = 1,    RecurrenceId = recurrenceIds["monthly"] }
            };
            foreach (var budget in budgetsHike)
            {
                var id = Guid.NewGuid();
                DataRow budgetRow = tableBudget.NewRow();
                budgetRow["Id"] = id;
                budgetRow["Amount"] = budget.Amount;
                budgetRow["Label"] = budget.Label;
                budgetRow["Lifetime"] = budget.Lifetime.HasValue ? budget.Lifetime.Value : DBNull.Value;
                budgetRow["RecurrenceId"] = budget.RecurrenceId.HasValue ? budget.RecurrenceId.Value : DBNull.Value;
                budgetRow["ExistenceId"] = budget.ExistenceId.HasValue ? budget.ExistenceId.Value : DBNull.Value;
                tableBudget.Rows.Add(budgetRow);

                DataRow categorizedAsRow = tableCategory.NewRow();
                categorizedAsRow["Id"] = Guid.NewGuid();
                categorizedAsRow["BudgetId"] = id;
                categorizedAsRow["CategoryId"] = categoryIds["hike"];
                tableCategory.Rows.Add(categorizedAsRow);
            }
            var budgetsShelter = new DbBudget[] {
                new DbBudget { Id = Guid.NewGuid(), Amount = -1000, ExistenceId = existenceIds["suggested"], Label = "home payment",          Lifetime = 1,    RecurrenceId = recurrenceIds["monthly"] },
                new DbBudget { Id = Guid.NewGuid(), Amount = 1000,  ExistenceId = existenceIds["suggested"], Label = "home lease",            Lifetime = 1,    RecurrenceId = recurrenceIds["monthly"] },
            };
            foreach (var budget in budgetsShelter)
            {
                var id = Guid.NewGuid();
                DataRow budgetRow = tableBudget.NewRow();
                budgetRow["Id"] = id;
                budgetRow["Amount"] = budget.Amount;
                budgetRow["Label"] = budget.Label;
                budgetRow["Lifetime"] = budget.Lifetime.HasValue ? budget.Lifetime.Value : DBNull.Value;
                budgetRow["RecurrenceId"] = budget.RecurrenceId.HasValue ? budget.RecurrenceId.Value : DBNull.Value;
                budgetRow["ExistenceId"] = budget.ExistenceId.HasValue ? budget.ExistenceId.Value : DBNull.Value;
                tableBudget.Rows.Add(budgetRow);

                DataRow categorizedAsRow = tableCategory.NewRow();
                categorizedAsRow["Id"] = Guid.NewGuid();
                categorizedAsRow["BudgetId"] = id;
                categorizedAsRow["CategoryId"] = categoryIds["shelter"];
                tableCategory.Rows.Add(categorizedAsRow);
            }

            keyValueStorage.SetItem("tbl_Budget", tableBudget);
            keyValueStorage.SetItem("tbl_Category", tableCategory);
        }
    }
}
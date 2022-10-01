using System.Data;

namespace Lifestyles.Infrastructure.Database.Budget.Models
{
    public class DbBudget
    {
        public Guid Id { get; set; }
        public Guid BudgetTypeId { get; set; }
        public decimal? Amount { get; set; }
        public string Label { get; set; }
        public decimal? Lifetime { get; set; }
        public Guid? RecurrenceId { get; set; }
        public Guid? ExistenceId { get; set; }

        public static void Default(
            IKeyValueStorage keyValueStorage,
            Dictionary<string, Guid> budgetTypeIds,
            Dictionary<string, Guid> lifestyleIds,
            Dictionary<string, Guid> categoryIds,
            Dictionary<string, Guid> recurrenceIds,
            Dictionary<string, Guid> existenceIds)
        {
            var tableBudget = keyValueStorage.GetItem<DataTable>("tbl_Budget");
            if (tableBudget == null)
            {
                tableBudget = new DataTable();
                tableBudget.Columns.Add("Id", typeof(Guid));
                tableBudget.Columns.Add(new DataColumn { ColumnName = "Amount", DataType = typeof(decimal), AllowDBNull = true });
                tableBudget.Columns.Add("Label", typeof(string));
                tableBudget.Columns.Add(new DataColumn { ColumnName = "Lifetime", DataType = typeof(decimal), AllowDBNull = true });
                tableBudget.Columns.Add(new DataColumn { ColumnName = "RecurrenceId", DataType = typeof(Guid), AllowDBNull = true });
                tableBudget.Columns.Add(new DataColumn { ColumnName = "ExistenceId", DataType = typeof(Guid), AllowDBNull = true });
            }
            var tableCategory = keyValueStorage.GetItem<DataTable>("tbl_Category");
            if (tableCategory == null)
            {
                tableCategory = new DataTable();
                tableCategory.Columns.Add("Id", typeof(Guid));
                tableCategory.Columns.Add("BudgetId", typeof(Guid));
                tableCategory.Columns.Add("CategoryId", typeof(Guid));
            }

            var AddBudget = (DbBudget budget) =>
            {
                DataRow budgetRow = tableBudget.NewRow();
                budgetRow["Id"] = budget.Id;
                budgetRow["BudgetTypeId"] = budget.BudgetTypeId;
                budgetRow["Amount"] = budget.Amount;
                budgetRow["Label"] = budget.Label;
                budgetRow["Lifetime"] = budget.Lifetime.HasValue ? budget.Lifetime.Value : DBNull.Value;
                budgetRow["RecurrenceId"] = budget.RecurrenceId;
                budgetRow["ExistenceId"] = budget.ExistenceId;
                tableBudget.Rows.Add(budgetRow);
            };
            var CategorizeAs = (DbBudget budget, KeyValuePair<string, Guid> idByLabel) =>
            {
                DataRow categorizedAsRow = tableCategory.NewRow();
                categorizedAsRow["Id"] = Guid.NewGuid();
                categorizedAsRow["BudgetId"] = budget.Id;
                categorizedAsRow["CategoryId"] = idByLabel.Value;
                tableCategory.Rows.Add(categorizedAsRow);
            };

            foreach (var lifestyleId in lifestyleIds)
            {
                foreach (var categoryId in categoryIds)
                {
                    if (lifestyleId.Key.Equals("Appalachian Trail") && categoryId.Key.Equals("eat & hydrate"))
                    {
                        new DbBudget[] {
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -40,   ExistenceId = existenceIds["expected"],  Label = "water filter",          Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -40,   ExistenceId = existenceIds["expected"],  Label = "groceries",             Lifetime = 4,    RecurrenceId = recurrenceIds["daily"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -50,   ExistenceId = existenceIds["expected"],  Label = "stove",                 Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -50,   ExistenceId = existenceIds["expected"],  Label = "pot",                   Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -10,   ExistenceId = existenceIds["expected"],  Label = "utensil",               Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -7,    ExistenceId = existenceIds["expected"],  Label = "stove fuel",            Lifetime = 2,    RecurrenceId = recurrenceIds["weekly"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -90,   ExistenceId = existenceIds["expected"],  Label = "food storage",          Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -25,   ExistenceId = existenceIds["expected"],  Label = "eating & drinking out", Lifetime = 4,    RecurrenceId = recurrenceIds["daily"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -12,   ExistenceId = existenceIds["expected"],  Label = "trowel",                Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                        }.ToList().ForEach(budget =>
                        {
                            AddBudget(budget);
                            CategorizeAs(budget, categoryId);
                            CategorizeAs(budget, lifestyleId);
                        });
                    }
                    else if (lifestyleId.Key.Equals("Appalachian Trail") && categoryId.Key.Equals("connect"))
                    {
                        new DbBudget[] {
                                new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -5,    ExistenceId = existenceIds["expected"],  Label = "whistle",               Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                                new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -65,   ExistenceId = existenceIds["expected"],  Label = "mobile service",        Lifetime = 1,    RecurrenceId = recurrenceIds["monthly"] },
                                new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -25,   ExistenceId = existenceIds["expected"],  Label = "power brick",           Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                                new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -25,   ExistenceId = existenceIds["suggested"], Label = "mobile phone",          Lifetime = 1,    RecurrenceId = recurrenceIds["monthly"] },
                                new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -15,   ExistenceId = existenceIds["suggested"], Label = "wall charger",          Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                        }.ToList().ForEach(budget =>
                        {
                            AddBudget(budget);
                            CategorizeAs(budget, categoryId);
                            CategorizeAs(budget, lifestyleId);
                        });
                    }
                    else if (lifestyleId.Key.Equals("Appalachian Trail") && categoryId.Key.Equals("hike"))
                    {
                        new DbBudget[] {
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -50,   ExistenceId = existenceIds["expected"],  Label = "headlamp",              Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -75,   ExistenceId = existenceIds["expected"],  Label = "trekking poles",        Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -220,  ExistenceId = existenceIds["expected"],  Label = "backpack",              Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -21,   ExistenceId = existenceIds["expected"],  Label = "compass",               Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -40,   ExistenceId = existenceIds["expected"],  Label = "maps",                  Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -20,   ExistenceId = existenceIds["expected"],  Label = "first aid kit",         Lifetime = 3,    RecurrenceId = recurrenceIds["monthly"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = 0,     ExistenceId = existenceIds["suggested"], Label = "health insurance",      Lifetime = 1,    RecurrenceId = recurrenceIds["monthly"] }
                        }.ToList().ForEach(budget =>
                        {
                            AddBudget(budget);
                            CategorizeAs(budget, categoryId);
                            CategorizeAs(budget, lifestyleId);
                        });
                    }
                    else if (lifestyleId.Key.Equals("Appalachian Trail") && categoryId.Key.Equals("wear"))
                    {
                        new DbBudget[] {
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -90,   ExistenceId = existenceIds["expected"],  Label = "trail shoes",           Lifetime = 2,    RecurrenceId = recurrenceIds["monthly"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -30,   ExistenceId = existenceIds["expected"],  Label = "socks",                 Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -30,   ExistenceId = existenceIds["expected"],  Label = "underwear",             Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -80,   ExistenceId = existenceIds["expected"],  Label = "base layer bottoms",    Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -50,   ExistenceId = existenceIds["expected"],  Label = "bottoms",               Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -90,   ExistenceId = existenceIds["expected"],  Label = "shell bottoms",         Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -80,   ExistenceId = existenceIds["expected"],  Label = "base layer top",        Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -35,   ExistenceId = existenceIds["expected"],  Label = "top",                   Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -225,  ExistenceId = existenceIds["expected"],  Label = "insulated top",         Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -90,   ExistenceId = existenceIds["expected"],  Label = "shell top",             Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -15,   ExistenceId = existenceIds["expected"],  Label = "head net",              Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -15,   ExistenceId = existenceIds["expected"],  Label = "beanie",                Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                        }.ToList().ForEach(budget =>
                        {
                            AddBudget(budget);
                            CategorizeAs(budget, categoryId);
                            CategorizeAs(budget, lifestyleId);
                        });
                    }
                    else if (lifestyleId.Key.Equals("Appalachian Trail") && categoryId.Key.Equals("shelter"))
                    {
                        new DbBudget[] {
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -400,  ExistenceId = existenceIds["expected"],  Label = "tent",                  Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -150,  ExistenceId = existenceIds["expected"],  Label = "sleeping bag",          Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -45,   ExistenceId = existenceIds["expected"],  Label = "sleeping pad",          Lifetime = null, RecurrenceId = recurrenceIds["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -35,   ExistenceId = existenceIds["expected"],  Label = "town lodging",          Lifetime = 4,    RecurrenceId = recurrenceIds["daily"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = -1000, ExistenceId = existenceIds["suggested"], Label = "home payment",          Lifetime = 1,    RecurrenceId = recurrenceIds["monthly"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = budgetTypeIds["budget"], Amount = 1000,  ExistenceId = existenceIds["suggested"], Label = "home lease",            Lifetime = 1,    RecurrenceId = recurrenceIds["monthly"] },
                        }.ToList().ForEach(budget =>
                        {
                            AddBudget(budget);
                            CategorizeAs(budget, categoryId);
                            CategorizeAs(budget, lifestyleId);
                        });
                    }
                }
            }
            keyValueStorage.SetItem("tbl_Budget", tableBudget);
            keyValueStorage.SetItem("tbl_Category", tableCategory);
        }
    }
}
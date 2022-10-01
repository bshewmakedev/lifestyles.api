using Lifestyles.Infrastructure.Database.Categorize.Models;
using Lifestyles.Infrastructure.Database.Live.Models;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Budget.Models
{
    public class DbBudget : DbLifestyle
    {
        public decimal? Amount { get; set; }

        public DbBudget() : base() { }

        public DbBudget(DataRow row) : base(row)
        {
            Amount = decimal.TryParse(row["Amount"].ToString() ?? "", out var amountParsed)
                ? amountParsed
                : null;
        }

        public static new DataRow AddDataRow(
            DataTable tableBudget,
            Dictionary<string, Guid> budgetTypeIds,
            DbBudget dbBudget)
        {
            DataRow budgetRow = tableBudget.NewRow();
            budgetRow["Id"] = dbBudget.Id;
            budgetRow["BudgetTypeId"] = budgetTypeIds["budget"];
            budgetRow["Amount"] = dbBudget.Amount;
            budgetRow["Label"] = dbBudget.Label;
            budgetRow["Lifetime"] = dbBudget.Lifetime.HasValue ? dbBudget.Lifetime : DBNull.Value;
            budgetRow["RecurrenceId"] = dbBudget.RecurrenceId;
            budgetRow["ExistenceId"] = dbBudget.ExistenceId;
            tableBudget.Rows.Add(budgetRow);

            return budgetRow;
        }

        public static void Default(
            IKeyValueStorage keyValueStorage,
            Dictionary<string, Guid> budgetTypeIds,
            Dictionary<string, Guid> lifestyleIds,
            Dictionary<string, Guid> categoryIds,
            Dictionary<string, Guid> recurrenceIds,
            Dictionary<string, Guid> existenceIds)
        {
            var tableBudget = CreateDataTable(keyValueStorage);
            var tableCategorized = DbCategorized.CreateDataTable(keyValueStorage);

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
                            AddDataRow(tableBudget, budgetTypeIds, budget);
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = categoryId.Value });
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = lifestyleId.Value });

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
                            AddDataRow(tableBudget, budgetTypeIds, budget);
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = categoryId.Value });
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = lifestyleId.Value });
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
                            AddDataRow(tableBudget, budgetTypeIds, budget);
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = categoryId.Value });
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = lifestyleId.Value });
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
                            AddDataRow(tableBudget, budgetTypeIds, budget);
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = categoryId.Value });
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = lifestyleId.Value });
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
                            AddDataRow(tableBudget, budgetTypeIds, budget);
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = categoryId.Value });
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = lifestyleId.Value });
                        });
                    }
                }
            }
            keyValueStorage.SetItem("tbl_Budget", tableBudget);
            keyValueStorage.SetItem("tbl_Categorized", tableCategorized);
        }
    }
}
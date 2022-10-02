using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Live.Repositories;
using BudgetMap = Lifestyles.Infrastructure.Database.Budget.Map.Budget;
using Lifestyles.Infrastructure.Database.Budget.Models;
using Lifestyles.Infrastructure.Database.Categorize.Models;
using Lifestyles.Infrastructure.Database.Live.Models;
using Lifestyles.Infrastructure.Database.Live.Extensions;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Budget.Repositories
{
    public class BudgetRepo : IBudgetRepo
    {
        private readonly IKeyValueStorage _context;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ILifestyleRepo _lifestyleRepo;

        public BudgetRepo(
            IKeyValueStorage context,
            ICategoryRepo categoryRepo,
            ILifestyleRepo lifestyleRepo)
        {
            _context = context;
            _categoryRepo = categoryRepo;
            _lifestyleRepo = lifestyleRepo;
        }

        public IEnumerable<IBudget> Default()
        {
            var budgets = new List<IBudget>();
            var dbBudgetTypeDict = DbBudgetType.CreateDataTable(_context).GetRows().Select(r => new DbBudgetType(r)).ToDictionary(t => t.Alias, t => t.Id);
            var dbRecurrenceDict = DbRecurrence.CreateDataTable(_context).GetRows().Select(r => new DbRecurrence(r)).ToDictionary(t => t.Alias, t => t.Id);
            var dbExistenceDict = DbExistence.CreateDataTable(_context).GetRows().Select(r => new DbExistence(r)).ToDictionary(t => t.Alias, t => t.Id);
            var tableBudget = DbBudget.CreateDataTable(_context);
            var tableCategorized = DbCategorized.CreateDataTable(_context);
            foreach (var lifestyle in _lifestyleRepo.Find())
            {
                foreach (var category in _categoryRepo.FindCategorizedAs(lifestyle.Id))
                {
                    if (lifestyle.Label.Equals("Appalachian Trail") && category.Label.Equals("eat & hydrate"))
                    {
                        new DbBudget[] {
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -40,   ExistenceId = dbExistenceDict["expected"],  Label = "water filter",          Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -40,   ExistenceId = dbExistenceDict["expected"],  Label = "groceries",             Lifetime = 4,    RecurrenceId = dbRecurrenceDict["daily"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -50,   ExistenceId = dbExistenceDict["expected"],  Label = "stove",                 Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -50,   ExistenceId = dbExistenceDict["expected"],  Label = "pot",                   Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -10,   ExistenceId = dbExistenceDict["expected"],  Label = "utensil",               Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -7,    ExistenceId = dbExistenceDict["expected"],  Label = "stove fuel",            Lifetime = 2,    RecurrenceId = dbRecurrenceDict["weekly"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -90,   ExistenceId = dbExistenceDict["expected"],  Label = "food storage",          Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -25,   ExistenceId = dbExistenceDict["expected"],  Label = "eating & drinking out", Lifetime = 4,    RecurrenceId = dbRecurrenceDict["daily"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -12,   ExistenceId = dbExistenceDict["expected"],  Label = "trowel",                Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                        }.ToList().ForEach(budget =>
                        {
                            DbBudget.AddDataRow(tableBudget, dbBudgetTypeDict, budget);
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = category.Id });
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = lifestyle.Id });
                        });
                    }
                    else if (lifestyle.Label.Equals("Appalachian Trail") && category.Label.Equals("connect"))
                    {
                        new DbBudget[] {
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -5,    ExistenceId = dbExistenceDict["expected"],  Label = "whistle",               Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -65,   ExistenceId = dbExistenceDict["expected"],  Label = "mobile service",        Lifetime = 1,    RecurrenceId = dbRecurrenceDict["monthly"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -25,   ExistenceId = dbExistenceDict["expected"],  Label = "power brick",           Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -25,   ExistenceId = dbExistenceDict["suggested"], Label = "mobile phone",          Lifetime = 1,    RecurrenceId = dbRecurrenceDict["monthly"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -15,   ExistenceId = dbExistenceDict["suggested"], Label = "wall charger",          Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                        }.ToList().ForEach(budget =>
                        {
                            DbBudget.AddDataRow(tableBudget, dbBudgetTypeDict, budget);
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = category.Id });
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = lifestyle.Id });
                        });
                    }
                    else if (lifestyle.Label.Equals("Appalachian Trail") && category.Label.Equals("hike"))
                    {
                        new DbBudget[] {
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -50,   ExistenceId = dbExistenceDict["expected"],  Label = "headlamp",              Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -75,   ExistenceId = dbExistenceDict["expected"],  Label = "trekking poles",        Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -220,  ExistenceId = dbExistenceDict["expected"],  Label = "backpack",              Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -21,   ExistenceId = dbExistenceDict["expected"],  Label = "compass",               Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -40,   ExistenceId = dbExistenceDict["expected"],  Label = "maps",                  Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -20,   ExistenceId = dbExistenceDict["expected"],  Label = "first aid kit",         Lifetime = 3,    RecurrenceId = dbRecurrenceDict["monthly"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = 0,     ExistenceId = dbExistenceDict["suggested"], Label = "health insurance",      Lifetime = 1,    RecurrenceId = dbRecurrenceDict["monthly"] }
                        }.ToList().ForEach(budget =>
                        {
                            DbBudget.AddDataRow(tableBudget, dbBudgetTypeDict, budget);
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = category.Id });
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = lifestyle.Id });
                        });
                    }
                    else if (lifestyle.Label.Equals("Appalachian Trail") && category.Label.Equals("wear"))
                    {
                        new DbBudget[] {
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -90,   ExistenceId = dbExistenceDict["expected"],  Label = "trail shoes",           Lifetime = 2,    RecurrenceId = dbRecurrenceDict["monthly"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -30,   ExistenceId = dbExistenceDict["expected"],  Label = "socks",                 Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -30,   ExistenceId = dbExistenceDict["expected"],  Label = "underwear",             Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -80,   ExistenceId = dbExistenceDict["expected"],  Label = "base layer bottoms",    Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -50,   ExistenceId = dbExistenceDict["expected"],  Label = "bottoms",               Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -90,   ExistenceId = dbExistenceDict["expected"],  Label = "shell bottoms",         Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -80,   ExistenceId = dbExistenceDict["expected"],  Label = "base layer top",        Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -35,   ExistenceId = dbExistenceDict["expected"],  Label = "top",                   Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -225,  ExistenceId = dbExistenceDict["expected"],  Label = "insulated top",         Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -90,   ExistenceId = dbExistenceDict["expected"],  Label = "shell top",             Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -15,   ExistenceId = dbExistenceDict["expected"],  Label = "head net",              Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -15,   ExistenceId = dbExistenceDict["expected"],  Label = "beanie",                Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                        }.ToList().ForEach(budget =>
                        {
                            DbBudget.AddDataRow(tableBudget, dbBudgetTypeDict, budget);
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = category.Id });
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = lifestyle.Id });
                        });
                    }
                    else if (lifestyle.Label.Equals("Appalachian Trail") && category.Label.Equals("shelter"))
                    {
                        new DbBudget[] {
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -400,  ExistenceId = dbExistenceDict["expected"],  Label = "tent",                  Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -150,  ExistenceId = dbExistenceDict["expected"],  Label = "sleeping bag",          Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -45,   ExistenceId = dbExistenceDict["expected"],  Label = "sleeping pad",          Lifetime = null, RecurrenceId = dbRecurrenceDict["never"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -35,   ExistenceId = dbExistenceDict["expected"],  Label = "town lodging",          Lifetime = 4,    RecurrenceId = dbRecurrenceDict["daily"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = -1000, ExistenceId = dbExistenceDict["suggested"], Label = "home payment",          Lifetime = 1,    RecurrenceId = dbRecurrenceDict["monthly"] },
                            new DbBudget { Id = Guid.NewGuid(), BudgetTypeId = dbBudgetTypeDict["budget"], Amount = 1000,  ExistenceId = dbExistenceDict["suggested"], Label = "home lease",            Lifetime = 1,    RecurrenceId = dbRecurrenceDict["monthly"] },
                        }.ToList().ForEach(budget =>
                        {
                            DbBudget.AddDataRow(tableBudget, dbBudgetTypeDict, budget);
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = category.Id });
                            DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = budget.Id, CategoryId = lifestyle.Id });
                        });
                    }
                }
            }
            _context.SetItem("tbl_Budget", tableBudget);
            _context.SetItem("tbl_Categorized", tableCategorized);

            return budgets;
        }

        public IEnumerable<IBudget> Find(Func<IBudget, bool>? predicate = null)
        {
            var dbBudgetTypes = DbBudgetType.CreateDataTable(_context)
                .GetRows()
                .Select(r => new DbBudgetType(r));
            var budgets = DbBudget.CreateDataTable(_context)
                .GetRows()
                .Select(r => new DbBudget(r))
                .Where(b => b.BudgetTypeId.Equals(
                    dbBudgetTypes.FirstOrDefault(bt => bt.Alias.Equals("budget"))?.Id))
                .Select(b => new BudgetMap(_context, b))
                .Where(predicate ?? ((b) => true));
            
            return budgets;
        }

        public IEnumerable<IBudget> FindCategorizedAs(Guid categoryId)
        {
            var dbCategories = DbCategorized.CreateDataTable(_context)
                .GetRows()
                .Where(r => (r["CategoryId"]?.ToString() ?? "").Equals(categoryId.ToString()));

            var dbBudgetTypes = DbBudgetType.CreateDataTable(_context)
                .GetRows()
                .Select(r => new DbBudgetType(r));
            var budgets = DbBudget.CreateDataTable(_context)
                .GetRows()
                .Select(r => new DbBudget(r))
                .Join(
                    dbCategories, 
                    b => b.Id, 
                    cr => Guid.Parse(cr["BudgetId"].ToString() ?? ""), (br, cr) => br)
                .Where(c => c.BudgetTypeId.Equals(
                    dbBudgetTypes.FirstOrDefault(bt => bt.Alias.Equals("budget"))?.Id))
                .Select(c => new BudgetMap(_context, c));
            
            return budgets;
        }

        public IEnumerable<IBudget> Upsert(IEnumerable<IBudget> budgets)
        {
            var budgetTable = DbBudget.CreateDataTable(_context);

            foreach (var budget in budgets)
            {
                foreach (DataRow budgetRow in budgetTable.Rows)
                {
                    if (Guid.Parse(budgetRow["Id"].ToString() ?? "").Equals(budget.Id))
                    {
                        budgetRow["BudgetTypeId"] = Guid.NewGuid();
                        budgetRow["Amount"] = budget.Amount * (int)budget.Direction;
                        budgetRow["Label"] = budget.Label;
                        budgetRow["Lifetime"] = budget.Lifetime.HasValue ? budget.Lifetime.Value : DBNull.Value;
                        budgetRow["RecurrenceId"] = Guid.NewGuid();
                        budgetRow["ExistenceId"] = Guid.NewGuid();
                    }
                }
            }

            return budgetTable.GetRows().Select(r => new BudgetMap(_context, new DbBudget(r)));
        }

        public IEnumerable<IBudget> Remove(IEnumerable<IBudget> budgets)
        {
            var budgetsDb = Find();

            return budgetsDb.Where(b => budgets.All(b2 => !b.Id.Equals(b2.Id)));
        }
    }
}
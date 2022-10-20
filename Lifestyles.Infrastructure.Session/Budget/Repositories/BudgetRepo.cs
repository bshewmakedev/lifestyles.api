using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Session.Budget.Models;
using Lifestyles.Infrastructure.Session.Categorize.Models;
using Lifestyles.Infrastructure.Session.Live.Models;
using Lifestyles.Infrastructure.Session.Live.Extensions;
using System.Data;
using Newtonsoft.Json;
using BudgetMap = Lifestyles.Infrastructure.Session.Budget.Map.Budget;

namespace Lifestyles.Infrastructure.Session.Budget.Repositories
{
    public class BudgetRepo : IBudgetRepo
    {
        private readonly IKeyValueRepo _context;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ILifestyleRepo _lifestyleRepo;

        public BudgetRepo(
            IKeyValueRepo context,
            ICategoryRepo categoryRepo,
            ILifestyleRepo lifestyleRepo)
        {
            _context = context;
            _categoryRepo = categoryRepo;
            _lifestyleRepo = lifestyleRepo;
        }

        private class BudgetJson
        {
            public int Amount { get; set; }
            public string CategoryLabel { get; set; }
            public string Label { get; set; }
            public int? Lifetime { get; set; }
            public string RecurrenceAlias { get; set; }
            public string ExistenceAlias { get; set; }
        }
        public IEnumerable<IBudget> Default()
        {
            var budgets = new List<IBudget>();
            var dbBudgetTypeDict = DbBudgetType.CreateDataTable(_context).GetRows().Select(r => new DbBudgetType(r)).ToDictionary(t => t.Alias, t => t.Id);
            var dbRecurrenceDict = DbRecurrence.CreateDataTable(_context).GetRows().Select(r => new DbRecurrence(r)).ToDictionary(t => t.Alias, t => t.Id);
            var dbExistenceDict = DbExistence.CreateDataTable(_context).GetRows().Select(r => new DbExistence(r)).ToDictionary(t => t.Alias, t => t.Id);
            var dbBudgets = new List<DbBudget>();
            var tableBudget = DbBudget.CreateDataTable(_context);
            var tableCategorized = DbCategorized.CreateDataTable(_context);
            foreach (var lifestyle in _lifestyleRepo.Find())
            {
                using (StreamReader reader = File.OpenText($"../Lifestyles.Domain/Budget/Defaults/Budgets.{lifestyle.Label.Replace("-", "").Replace(" ", "")}.json"))
                {
                    var tempBudgets = JsonConvert
                        .DeserializeObject<List<BudgetJson>>(reader.ReadToEnd());
                    foreach (var category in _categoryRepo.FindCategorizedAs(lifestyle.Id))
                    {
                        tempBudgets
                            .Where(b => b.CategoryLabel.Equals(category.Label))
                            .Select(j => new DbBudget
                            {
                                Id = Guid.NewGuid(),
                                Amount = j.Amount,
                                Label = j.Label,
                                Lifetime = j.Lifetime,
                                RecurrenceId = dbRecurrenceDict[j.RecurrenceAlias],
                                ExistenceId = dbExistenceDict[j.ExistenceAlias]
                            })
                            .ToList()
                            .ForEach(b =>
                            {
                                DbBudget.AddDataRow(tableBudget, dbBudgetTypeDict, b);
                                DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = b.Id, CategoryId = category.Id });
                                DbCategorized.AddDataRow(tableCategorized, new DbCategorized { Id = Guid.NewGuid(), BudgetId = b.Id, CategoryId = lifestyle.Id });
                                budgets.Add(new BudgetMap(_context, b));
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
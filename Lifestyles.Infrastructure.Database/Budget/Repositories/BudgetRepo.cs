using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;
using BudgetMap = Lifestyles.Infrastructure.Database.Budget.Map.Budget;
using Lifestyles.Infrastructure.Database.Budget.Models;
using Lifestyles.Infrastructure.Database.Live.Extensions;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Budget.Repositories
{
    public class BudgetRepo : IBudgetRepo
    {
        private readonly IKeyValueStorage _context;

        public BudgetRepo(IKeyValueStorage context)
        {
            _context = context;
        }

        public IEnumerable<IBudget> Find(Func<IBudget, bool>? predicate = null)
        {
            var dbBudgetTypes = _context.GetItem<DataTable>("tbl_BudgetType")
                .GetRows()
                .Select(r => new DbBudgetType(r));
            var budgets = _context.GetItem<DataTable>("tbl_Budget")
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
            var dbCategories = _context.GetItem<DataTable>("tbl_Categorized")
                .GetRows()
                .Where(r => (r["CategoryId"]?.ToString() ?? "").Equals(categoryId.ToString()));

            var dbBudgetTypes = _context.GetItem<DataTable>("tbl_BudgetType")
                .GetRows()
                .Select(r => new DbBudgetType(r));
            var budgets = _context.GetItem<DataTable>("tbl_Budget")
                .GetRows()
                .Select(r => new DbBudget(r))
                .Join(
                    dbCategories, 
                    b => b.Id, 
                    cr => Guid.Parse(cr["BudgetId"].ToString() ?? ""), (br, cr) => br)
                .Where(c => c.BudgetTypeId.Equals(
                    dbBudgetTypes.FirstOrDefault(bt => bt.Alias.Equals("category"))?.Id))
                .Select(c => new BudgetMap(_context, c));
            
            return budgets;
        }

        public IEnumerable<IBudget> Upsert(IEnumerable<IBudget> budgets)
        {
            var budgetTable = _context.GetItem<DataTable>("tbl_Budget");

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
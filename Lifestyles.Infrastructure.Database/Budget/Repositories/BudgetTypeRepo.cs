using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Infrastructure.Database.Budget.Models;
using Lifestyles.Infrastructure.Database.Live.Extensions;
using System.Data;
using BudgetTypeEnum = Lifestyles.Domain.Budget.Constants.BudgetType;
using BudgetTypeMap = Lifestyles.Domain.Budget.Map.BudgetType;

namespace Lifestyles.Infrastructure.Database.Budget.Repositories
{
    public class BudgetTypeRepo : IBudgetTypeRepo
    {
        private readonly IKeyValueStorage _context;

        public BudgetTypeRepo(IKeyValueStorage context)
        {
            _context = context;
        }

        public IEnumerable<BudgetTypeEnum> Default()
        {
            var budgetTypes = new List<BudgetTypeEnum>();
            var tableBudgetType = DbBudgetType.CreateDataTable(_context);
            foreach (var dbBudgetType in new DbBudgetType[] {
                new DbBudgetType { Id = Guid.NewGuid(), Alias = "budget" },
                new DbBudgetType { Id = Guid.NewGuid(), Alias = "category" },
                new DbBudgetType { Id = Guid.NewGuid(), Alias = "lifestyle" }
            })
            {
                DbBudgetType.AddDataRow(tableBudgetType, dbBudgetType);
                budgetTypes.Add(BudgetTypeMap.Map(dbBudgetType.Alias));
            }
            _context.SetItem("tbl_BudgetType", tableBudgetType);

            return budgetTypes;
        }

        public IEnumerable<BudgetTypeEnum> Find(Func<BudgetTypeEnum, bool>? predicate = null)
        {
            var dbBudgetTypes = DbBudgetType.CreateDataTable(_context)
                .GetRows()
                .Select(r => new DbBudgetType(r));
            
            return dbBudgetTypes.Select(t => BudgetTypeMap.Map(t.Alias));
        }
        
        public IEnumerable<BudgetTypeEnum> Upsert(IEnumerable<BudgetTypeEnum> budgets)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<BudgetTypeEnum> Remove(IEnumerable<BudgetTypeEnum> budgets)
        {
            throw new System.NotImplementedException();
        }
    }
}
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Infrastructure.Session.Budget.Models;
using Lifestyles.Infrastructure.Session.Live.Extensions;
using System.Data;
using BudgetTypeEnum = Lifestyles.Domain.Budget.Entities.BudgetType;
using BudgetTypeMap = Lifestyles.Service.Budget.Map.BudgetType;

namespace Lifestyles.Infrastructure.Session.Budget.Repositories
{
    public class BudgetTypeRepo : IBudgetTypeRepo
    {
        private readonly IKeyValueRepo _context;

        public BudgetTypeRepo(IKeyValueRepo context)
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
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Repositories;
using LifestyleMap = Lifestyles.Infrastructure.Database.Live.Map.Lifestyle;
using Lifestyles.Infrastructure.Database.Live.Models;
using Lifestyles.Infrastructure.Database.Budget.Models;
using Lifestyles.Infrastructure.Database.Live.Extensions;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Live.Repositories
{
    public class LifestyleRepo : ILifestyleRepo
    {
        private readonly IKeyValueRepo _context;

        public LifestyleRepo(IKeyValueRepo context)
        {
            _context = context;
        }

        public IEnumerable<ILifestyle> Default()
        {
            var lifestyles = new List<ILifestyle>();

            var dbBudgetTypeDict = DbBudgetType.CreateDataTable(_context).GetRows().Select(r => new DbBudgetType(r)).ToDictionary(t => t.Alias, t => t.Id);
            var dbRecurrenceDict = DbRecurrence.CreateDataTable(_context).GetRows().Select(r => new DbRecurrence(r)).ToDictionary(t => t.Alias, t => t.Id);
            var dbExistenceDict = DbExistence.CreateDataTable(_context).GetRows().Select(r => new DbExistence(r)).ToDictionary(t => t.Alias, t => t.Id);
            var tableBudget = DbLifestyle.CreateDataTable(_context);
            new List<DbLifestyle> {
                new DbLifestyle { Id = Guid.NewGuid(), Label = "Appalachian Trail",        Lifetime = 6, RecurrenceId = dbRecurrenceDict["monthly"], ExistenceId = dbExistenceDict["expected"] },
                new DbLifestyle { Id = Guid.NewGuid(), Label = "Pacific Crest Trail",      Lifetime = 5, RecurrenceId = dbRecurrenceDict["monthly"], ExistenceId = dbExistenceDict["expected"] },
                new DbLifestyle { Id = Guid.NewGuid(), Label = "Continental Divide Trail", Lifetime = 6, RecurrenceId = dbRecurrenceDict["monthly"], ExistenceId = dbExistenceDict["expected"] },
            }.ForEach(dbLifestyle =>
            {
                DbLifestyle.AddDataRow(tableBudget, dbBudgetTypeDict, dbLifestyle);
                lifestyles.Add(new LifestyleMap(_context, dbLifestyle));
            });
            _context.SetItem("tbl_Budget", tableBudget);

            return lifestyles;
        }

        public IEnumerable<ILifestyle> Find(Func<ILifestyle, bool>? predicate = null)
        {
            var dbBudgetTypes = DbBudgetType.CreateDataTable(_context)
                .GetRows()
                .Select(r => new DbBudgetType(r));
            var lifestyles = DbBudget.CreateDataTable(_context)
                .GetRows()
                .Select(r => new DbLifestyle(r))
                .Where(l => l.BudgetTypeId.Equals(
                    dbBudgetTypes.FirstOrDefault(bt => bt.Alias.Equals("lifestyle"))?.Id))
                .Select(l => new LifestyleMap(_context, l))
                .Where(predicate ?? ((b) => true));

            return lifestyles;
        }

        public IEnumerable<ILifestyle> Upsert(IEnumerable<ILifestyle> lifestyles)
        {
            return lifestyles;
        }

        public IEnumerable<ILifestyle> Remove(IEnumerable<ILifestyle> lifestyles)
        {
            var lifestylesDb = Find();

            return lifestylesDb.Where(b => lifestyles.All(b2 => !b.Id.Equals(b2.Id)));
        }
    }
}
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Repositories;
using LifestyleMap = Lifestyles.Infrastructure.Session.Live.Map.Lifestyle;
using Lifestyles.Infrastructure.Session.Live.Models;
using Lifestyles.Infrastructure.Session.Budget.Models;
using Lifestyles.Infrastructure.Session.Live.Extensions;
using DefaultLifestyleRepo = Lifestyles.Infrastructure.Default.Live.Repositories.LifestyleRepo;
using Newtonsoft.Json;
using System.Data;

namespace Lifestyles.Infrastructure.Session.Live.Repositories
{
    public class LifestyleRepo : ILifestyleRepo
    {
        private readonly IKeyValueRepo _context;
        private readonly DefaultLifestyleRepo _defaultLifestyleRepo;

        public LifestyleRepo(
            IKeyValueRepo context,
            DefaultLifestyleRepo defaultLifestyleRepo)
        {
            _context = context;
            _defaultLifestyleRepo = defaultLifestyleRepo;

            Default();
        }

        private IEnumerable<ILifestyle> Default()
        {
            var dbRecurrenceDict = DbRecurrence.CreateDataTable(_context).GetRows().Select(r => new DbRecurrence(r)).ToDictionary(t => t.Alias, t => t.Id);
            var dbExistenceDict = DbExistence.CreateDataTable(_context).GetRows().Select(r => new DbExistence(r)).ToDictionary(t => t.Alias, t => t.Id);
            var dbLifestyles = _defaultLifestyleRepo
                .Find()
                .Select(j => new DbLifestyle {
                    Id = Guid.NewGuid(),
                    Label = j.Label,
                    Lifetime = j.Lifetime,
                    RecurrenceId = dbRecurrenceDict[j.RecurrenceAlias],
                    ExistenceId = dbExistenceDict[j.ExistenceAlias]
                }).ToList();

            var lifestyles = new List<ILifestyle>();
            var dbBudgetTypeDict = DbBudgetType.CreateDataTable(_context).GetRows().Select(r => new DbBudgetType(r)).ToDictionary(t => t.Alias, t => t.Id);
            var tableBudget = DbLifestyle.CreateDataTable(_context);
            dbLifestyles.ForEach(dbLifestyle =>
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
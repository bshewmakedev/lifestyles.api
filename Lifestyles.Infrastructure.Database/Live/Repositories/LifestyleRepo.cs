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
        private readonly IKeyValueStorage _context;

        public LifestyleRepo(IKeyValueStorage context)
        {
            _context = context;
        }

        public IEnumerable<ILifestyle> Find(Func<ILifestyle, bool>? predicate = null)
        {
            var dbBudgetTypes = _context.GetItem<DataTable>("tbl_BudgetType")
                .GetRows()
                .Select(r => new DbBudgetType(r));
            var lifestyles = _context.GetItem<DataTable>("tbl_Budget")
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
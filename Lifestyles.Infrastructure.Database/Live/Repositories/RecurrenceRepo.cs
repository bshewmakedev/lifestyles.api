using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Database.Live.Models;
using Lifestyles.Infrastructure.Database.Live.Extensions;
using System.Data;
using RecurrenceEnum = Lifestyles.Domain.Live.Constants.Recurrence;
using RecurrenceMap = Lifestyles.Domain.Live.Map.Recurrence;

namespace Lifestyles.Infrastructure.Database.Live.Repositories
{
    public class RecurrenceRepo : IRecurrenceRepo
    {
        private readonly IKeyValueStorage _context;

        public RecurrenceRepo(IKeyValueStorage context)
        {
            _context = context;
        }

        public IEnumerable<RecurrenceEnum> Default()
        {
            var recurrences = new List<RecurrenceEnum>();
            var tableRecurrence = DbRecurrence.CreateDataTable(_context);
            foreach (var dbRecurrence in new DbRecurrence[] {
                new DbRecurrence { Id = Guid.NewGuid(), Alias = "never" },
                new DbRecurrence { Id = Guid.NewGuid(), Alias = "daily" },
                new DbRecurrence { Id = Guid.NewGuid(), Alias = "weekly" },
                new DbRecurrence { Id = Guid.NewGuid(), Alias = "monthly" },
                new DbRecurrence { Id = Guid.NewGuid(), Alias = "annually" }
            })
            {
                DbRecurrence.AddDataRow(tableRecurrence, dbRecurrence);
                recurrences.Add(RecurrenceMap.Map(dbRecurrence.Alias));
            }
            _context.SetItem("tbl_Recurrence", tableRecurrence);

            return recurrences;
        }

        public IEnumerable<RecurrenceEnum> Find(Func<RecurrenceEnum, bool>? predicate = null)
        {
            var dbRecurrences = DbRecurrence.CreateDataTable(_context)
                .GetRows()
                .Select(r => new DbRecurrence(r));

            return dbRecurrences.Select(t => RecurrenceMap.Map(t.Alias));
        }

        public IEnumerable<RecurrenceEnum> Upsert(IEnumerable<RecurrenceEnum> recurrences)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<RecurrenceEnum> Remove(IEnumerable<RecurrenceEnum> recurrences)
        {
            throw new System.NotImplementedException();
        }
    }
}
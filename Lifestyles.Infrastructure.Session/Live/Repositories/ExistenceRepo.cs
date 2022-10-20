using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Session.Live.Models;
using Lifestyles.Infrastructure.Session.Live.Extensions;
using System.Data;
using ExistenceEnum = Lifestyles.Domain.Live.Entities.Existence;
using ExistenceMap = Lifestyles.Service.Live.Map.Existence;

namespace Lifestyles.Infrastructure.Session.Live.Repositories
{
    public class ExistenceRepo : IExistenceRepo
    {
        private readonly IKeyValueRepo _context;

        public ExistenceRepo(IKeyValueRepo context)
        {
            _context = context;
        }

        public IEnumerable<ExistenceEnum> Default()
        {
            var existences = new List<ExistenceEnum>();
            var tableExistence = DbExistence.CreateDataTable(_context);
            foreach (var dbExistence in new DbExistence[] {
                new DbExistence { Id = Guid.NewGuid(), Alias = "excluded" },
                new DbExistence { Id = Guid.NewGuid(), Alias = "expected" },
                new DbExistence { Id = Guid.NewGuid(), Alias = "suggested" }
            })
            {
                DbExistence.AddDataRow(tableExistence, dbExistence);
                existences.Add(ExistenceMap.Map(dbExistence.Alias));
            }
            _context.SetItem("tbl_Existence", tableExistence);

            return existences;
        }

        public IEnumerable<ExistenceEnum> Find(Func<ExistenceEnum, bool>? predicate = null)
        {
            var dbExistences = DbExistence.CreateDataTable(_context)
                .GetRows()
                .Select(r => new DbExistence(r));

            return dbExistences.Select(t => ExistenceMap.Map(t.Alias));
        }

        public IEnumerable<ExistenceEnum> Upsert(IEnumerable<ExistenceEnum> existences)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ExistenceEnum> Remove(IEnumerable<ExistenceEnum> existences)
        {
            throw new System.NotImplementedException();
        }
    }
}
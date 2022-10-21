using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Repositories;

namespace Lifestyles.Infrastructure.Session.Live.Repositories
{
    public class LifestyleRepo : ILifestyleRepo
    {
        public IEnumerable<ILifestyle> Find(Func<ILifestyle, bool>? predicate = null)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ILifestyle> Upsert(IEnumerable<ILifestyle> entities)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ILifestyle> Remove(IEnumerable<ILifestyle> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}
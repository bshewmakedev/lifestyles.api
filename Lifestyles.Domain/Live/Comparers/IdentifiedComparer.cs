using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Domain.Live.Comparers
{
    public class IdentifiedComparer<TEntity> : IEqualityComparer<TEntity>
        where TEntity : IIdentified
    {
        public bool Equals(TEntity x, TEntity y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(TEntity x)
        {
            return x.Id.GetHashCode();
        }
    }
}
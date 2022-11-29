using Lifestyles.Domain.Categorize.Entities;

namespace Lifestyles.Domain.Categorize.Comparers
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
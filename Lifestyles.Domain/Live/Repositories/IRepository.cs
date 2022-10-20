namespace Lifestyles.Domain.Live.Repositories
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Find(Func<TEntity, bool>? predicate = null);
        IEnumerable<TEntity> Upsert(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> Remove(IEnumerable<TEntity> entities);
    }
}
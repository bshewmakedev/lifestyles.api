namespace Lifestyles.Domain.Live.Repositories
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Find(Func<TEntity, bool>? predicate);
        IEnumerable<TEntity> Upsert(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> Remove(IEnumerable<TEntity> entities);
    }
}
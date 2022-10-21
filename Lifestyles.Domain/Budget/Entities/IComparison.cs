namespace Lifestyles.Domain.Budget.Entities
{
    public interface IComparison<TEntity>
    {
        TEntity Entity { get; set; }
        int Ratio { get; set; }
        int Compare(IEnumerable<TEntity> entities);
    }
}
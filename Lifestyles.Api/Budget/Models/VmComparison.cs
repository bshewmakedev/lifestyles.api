using Lifestyles.Domain.Budget.Entities;

namespace Lifestyles.Api.Budget.Models
{
    public class VmComparison<TVmEntity, TEntity>
    {
        public TVmEntity Entity { get; set; }
        int Ratio { get; set; }

        public VmComparison(IComparison<TEntity> comparison)
        {
            Entity = (TVmEntity)Activator.CreateInstance(typeof(TVmEntity), comparison.Entity);
            Ratio = comparison.Ratio;
        }
    }
}
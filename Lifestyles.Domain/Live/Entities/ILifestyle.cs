using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Domain.Live.Entities
{
    public interface ILifestyle : ICategory, IRecur, IExist
    { 
        decimal GetSignedAmount(
            IEnumerable<IBudget> budgets,
            int? interval = null);
    }
}
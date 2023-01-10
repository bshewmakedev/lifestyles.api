using System.Collections.Generic;
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;

namespace Lifestyles.Domain.Live.Entities
{
    public interface ILifestyle : ICategory, IRecur, IExist
    { 
        decimal GetValue(
            IEnumerable<IBudget> budgets,
            int? interval = null);
    }
}
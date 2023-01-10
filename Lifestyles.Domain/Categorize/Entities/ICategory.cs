using System.Collections.Generic;
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Node.Entities;

namespace Lifestyles.Domain.Categorize.Entities
{
    public interface ICategory : IEntity
    {
        decimal GetValue(
            IRecur recur,
            IEnumerable<IBudget> budgets,
            int? interval = null);
    }
}
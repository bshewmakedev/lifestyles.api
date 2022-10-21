using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;

namespace Lifestyles.Infrastructure.Session.Budget.Repositories
{
    public class BudgetRepo : IBudgetRepo
    {
        public IEnumerable<IBudget> Find(Func<IBudget, bool>? predicate = null)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IBudget> FindCategorizedAs(Guid categoryId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IBudget> Upsert(IEnumerable<IBudget> entities)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IBudget> Remove(IEnumerable<IBudget> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}
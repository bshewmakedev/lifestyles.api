using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Services;

namespace Lifestyles.Service.Live.Services
{
    public class LiveService : ILiveService
    {
        public IEnumerable<Direction> FindDirections()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Recurrence> FindRecurrences()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Existence> FindExistences()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ILifestyle> FindLifestyles()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ILifestyle> UpsertLifestyles(IEnumerable<ILifestyle> budgets)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ILifestyle> RemoveLifestyles(IEnumerable<ILifestyle> budgets)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IComparison<ILifestyle>> CompareLifestyles(IEnumerable<ILifestyle> lifestyles)
        {
            throw new System.NotImplementedException();
        }
    }
}
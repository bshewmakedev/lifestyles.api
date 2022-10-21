using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Domain.Live.Services;

namespace Lifestyles.Service.Live.Services
{
    public class LiveService : ILiveService
    {
        private readonly ILifestyleRepo _lifestyleRepo;

        public LiveService(ILifestyleRepo lifestyleRepo)
        {
            _lifestyleRepo = lifestyleRepo;
        }
        
        public IEnumerable<Direction> FindDirections()
        {
            return Enum.GetValues(typeof(Direction)).Cast<Direction>();
        }

        public IEnumerable<Recurrence> FindRecurrences()
        {
            return Enum.GetValues(typeof(Recurrence)).Cast<Recurrence>();
        }

        public IEnumerable<Existence> FindExistences()
        {
            return Enum.GetValues(typeof(Existence)).Cast<Existence>();
        }

        public IEnumerable<ILifestyle> FindLifestyles()
        {
            return _lifestyleRepo.Find();
        }

        public IEnumerable<ILifestyle> UpsertLifestyles(IEnumerable<ILifestyle> lifestyles)
        {
            return _lifestyleRepo.Upsert(lifestyles);
        }

        public IEnumerable<ILifestyle> RemoveLifestyles(IEnumerable<ILifestyle> lifestyles)
        {
            return _lifestyleRepo.Remove(lifestyles);
        }

        public IEnumerable<IComparison<ILifestyle>> CompareLifestyles(IEnumerable<ILifestyle> lifestyles)
        {
            throw new System.NotImplementedException();
        }
    }
}
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Domain.Live.Services;

namespace Lifestyles.Service.Live.Services
{
    public class LiveService : ILiveService
    {
        private readonly IBudgetRepo _budgetRepo;
        private readonly ILifestyleRepo _lifestyleRepo;

        public LiveService(
            IBudgetRepo budgetRepo,
            ILifestyleRepo lifestyleRepo)
        {
            _budgetRepo = budgetRepo;
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

        public decimal GetSignedAmount(Guid lifestyleId)
        {
            var lifestyle = FindLifestyles().FirstOrDefault(l => l.Id.Equals(lifestyleId));

            if (lifestyle == null)
            {
                // TODO : Log a message.
                throw new NullReferenceException();
            }

            var budgets = _budgetRepo.FindCategorizedAs(lifestyleId);

            return lifestyle.GetSignedAmount(budgets);
        }

        public IEnumerable<IComparison<ILifestyle>> CompareLifestyles(IEnumerable<ILifestyle> lifestyles)
        {
            throw new System.NotImplementedException();
        }
    }
}
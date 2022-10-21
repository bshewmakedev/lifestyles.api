using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Domain.Live.Services
{
    public interface ILiveService
    {
        IEnumerable<Direction> FindDirections();
        IEnumerable<Recurrence> FindRecurrences();
        IEnumerable<Existence> FindExistences();
        IEnumerable<ILifestyle> FindLifestyles();
        IEnumerable<ILifestyle> UpsertLifestyles(IEnumerable<ILifestyle> lifestyles);
        IEnumerable<ILifestyle> RemoveLifestyles(IEnumerable<ILifestyle> lifestyles);
        decimal GetSignedAmount(Guid lifestyleId);
        IEnumerable<IComparison<ILifestyle>> CompareLifestyles(IEnumerable<ILifestyle> lifestyles);
    }
}
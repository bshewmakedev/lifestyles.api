using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Domain.Live.Services
{
    public interface ILiveService
    {
        IEnumerable<Recurrence> FindRecurrences();
        IEnumerable<Existence> FindExistences();
        IEnumerable<INode<IBudget>> FindDefaultLifeTrees();
        IEnumerable<INode<IBudget>> FindSavedLifeTrees();
        IEnumerable<INode<IBudget>> UpsertSavedLifeTrees(IEnumerable<INode<IBudget>> lifeTrees);
        IEnumerable<IComparison<ILifestyle>> CompareLifestyles(IEnumerable<ILifestyle> lifestyles);
    }
}
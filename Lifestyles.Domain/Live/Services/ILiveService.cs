using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Tree.Entities;

namespace Lifestyles.Domain.Live.Services
{
    public interface ILiveService
    {
        IEnumerable<Recurrence> FindRecurrences();
        IEnumerable<Existence> FindExistences();
        IEnumerable<Node<IBudget>> FindDefaultLifeTrees();
        IEnumerable<Node<IBudget>> FindSavedLifeTrees(Guid[]? lifestyleIds = null);
        IEnumerable<Node<IBudget>> UpsertSavedLifeTrees(IEnumerable<Node<IBudget>> lifeTrees);
        IEnumerable<IComparison<ILifestyle>> CompareLifestyles(IEnumerable<ILifestyle> lifestyles);
    }
}
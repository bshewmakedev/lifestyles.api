using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Measure.Constants;

namespace Lifestyles.Domain.Budget.Entities
{
    public partial interface IBudget : ICategory
    {
        decimal Amount { get; }
        Direction Direction { get; }
        void Value(decimal amount);
    }

    public partial interface IBudget
    {
        IEnumerable<ICategory> Categories { get; }
        void RecategorizeAs(IEnumerable<ICategory> categories);
        void DecategorizeAs(IEnumerable<ICategory> categories);
    }

    public partial interface IBudget
    {
        Existence Existence { get; }
        void Exclude();
        void Expect();
        void Suggest();
    }

    public partial interface IBudget 
    {
        string Label { get; }
        void Relabel(string label = "");
    }

    public partial interface IBudget
    {
        decimal? Lifetime { get; }
        Recurrence Recurrence { get; }
        void Recur(Recurrence recurrence, decimal? lifetime = null);
    }
}
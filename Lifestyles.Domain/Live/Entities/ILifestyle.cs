using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Constants;

namespace Lifestyles.Domain.Live.Entities
{
    public partial interface ILifestyle : ICategory
    {
        IEnumerable<ICategory> Categories { get; }
        void RecategorizeAs(IEnumerable<ICategory> categories);
        void DecategorizeAs(IEnumerable<ICategory> categories);
    }

    public partial interface ILifestyle
    {
        Existence Existence { get; }
        void Exist(Existence existence);
    }

    public partial interface ILifestyle
    {
        decimal? Lifetime { get; }
        Recurrence Recurrence { get; }
        void Recur(Recurrence recurrence, decimal? lifetime = null);
    }
}
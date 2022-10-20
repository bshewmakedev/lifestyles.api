using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Domain.Budget.Entities
{
    public partial interface IBudget : ILifestyle
    {
        decimal Amount { get; }
        Direction Direction { get; }
        void Value(decimal amount = 0);
    }
}
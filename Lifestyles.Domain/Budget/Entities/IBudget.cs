using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Node.Entities;

namespace Lifestyles.Domain.Budget.Entities
{
    public interface IBudget : ILifestyle, IEntity
    { 
        decimal Value { get; }
        decimal Momentum { get; }

        IBudget Valuate(
            decimal value = 0.0m, 
            decimal momentum = 0.0m);
    }
}
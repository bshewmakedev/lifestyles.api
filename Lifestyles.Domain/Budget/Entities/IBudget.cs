using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Domain.Budget.Entities
{
    public interface IBudget : ILifestyle
    { 
        decimal Value { get; }
        decimal Valuate(decimal value = 0);
    }
}
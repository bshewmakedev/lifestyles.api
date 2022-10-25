namespace Lifestyles.Domain.Live.Entities
{
    public interface IValued
    {
        decimal Amount { get; }
        Direction Direction { get; }
        void Value(decimal amount = 0);
    }
}
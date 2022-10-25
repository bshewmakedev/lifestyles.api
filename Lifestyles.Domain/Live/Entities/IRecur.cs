namespace Lifestyles.Domain.Live.Entities
{
    public interface IRecur
    {
        int? Lifetime { get; }
        Recurrence Recurrence { get; }
        void Recur(Recurrence recurrence = Recurrence.Never, int? lifetime = null);
    }
}
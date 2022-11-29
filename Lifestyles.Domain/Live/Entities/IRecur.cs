namespace Lifestyles.Domain.Live.Entities
{
    public interface IRecur
    {
        int? Lifetime { get; }
        Recurrence Recurrence { get; }
        IRecur Recur(Recurrence recurrence = Recurrence.Never, int? lifetime = null);
    }
}
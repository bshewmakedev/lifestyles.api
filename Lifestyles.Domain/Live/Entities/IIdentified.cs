namespace Lifestyles.Domain.Live.Entities
{
    public interface IIdentified
    {
        Guid Id { get; }

        void Identify(Guid? id);
    }
}
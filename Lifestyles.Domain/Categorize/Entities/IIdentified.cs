namespace Lifestyles.Domain.Categorize.Entities
{
    public interface IIdentified
    {
        Guid Id { get; }
        void Identify(Guid? id = null);
    }
}
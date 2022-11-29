namespace Lifestyles.Domain.Categorize.Entities
{
    public interface IIdentified
    {
        Guid Id { get; }
        Guid Identify(Guid? id = null);
    }
}
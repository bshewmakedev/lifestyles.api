namespace Lifestyles.Domain.Live.Entities
{
    public interface IExist
    {
        Existence Existence { get; }
        Existence Exist(Existence existence = Existence.Expected);
    }
}
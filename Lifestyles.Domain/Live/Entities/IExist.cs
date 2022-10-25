namespace Lifestyles.Domain.Live.Entities
{
    public interface IExist
    {
        Existence Existence { get; }
        void Exist(Existence existence = Existence.Expected);
    }
}
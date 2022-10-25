namespace Lifestyles.Domain.Live.Entities
{
    public interface ILabelled
    {
        string Label { get; }
        void Relabel(string label = "");
    }
}
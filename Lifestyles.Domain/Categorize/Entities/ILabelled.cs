namespace Lifestyles.Domain.Categorize.Entities
{
    public interface ILabelled
    {
        string Label { get; }
        string Relabel(string label = "");
    }
}
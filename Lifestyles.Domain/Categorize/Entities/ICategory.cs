using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Domain.Categorize.Entities
{
    public partial interface ICategory : IIdentified
    {
        string Label { get; }        
        void Relabel(string label = "");
    }
}
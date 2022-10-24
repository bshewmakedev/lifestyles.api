using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Service.Live.Models
{
    public static class DefaultExistence
    {
        public static string Map(this Existence existence)
        {
            switch (existence)
            {
                case Existence.Expected: return "expected";
                case Existence.Suggested: return "suggested";
                default: return "excluded";
            }
        }
    }
}
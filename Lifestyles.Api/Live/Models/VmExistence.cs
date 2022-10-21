using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Api.Live.Models
{
    public static class VmExistence
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
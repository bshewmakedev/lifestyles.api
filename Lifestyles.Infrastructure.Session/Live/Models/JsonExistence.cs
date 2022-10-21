using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Infrastructure.Session.Live.Models
{
    public static class JsonExistence
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
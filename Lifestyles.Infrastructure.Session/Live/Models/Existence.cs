using ExistenceEnum = Lifestyles.Domain.Live.Entities.Existence;

namespace Lifestyles.Infrastructure.Session.Live.Models
{
    public static class Existence
    {
        public const string Expected = "expected";
        public const string Suggested = "suggested";
        public const string Excluded = "excluded";

        public static string Map(this ExistenceEnum existence)
        {
            switch (existence)
            {
                case ExistenceEnum.Expected: return Expected;
                case ExistenceEnum.Suggested: return Suggested;
                default: return Excluded;
            }
        }
    }
}
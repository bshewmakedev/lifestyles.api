using ExistenceEnum = Lifestyles.Domain.Live.Entities.Existence;

namespace Lifestyles.Domain.Live.Map
{
    public static class Existence
    {
        public const string Expected = "expected";
        public const string Suggested = "suggested";
        public const string Excluded = "excluded";

        public static ExistenceEnum Map(this string existence)
        {
            switch (existence)
            {
                case Expected: return ExistenceEnum.Expected;
                case Suggested: return ExistenceEnum.Suggested;
                default: return ExistenceEnum.Excluded;
            }
        }

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
using ExistenceEnum = Lifestyles.Domain.Live.Entities.Existence;

namespace Lifestyles.Infrastructure.Session.Live.Map
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
    }
}
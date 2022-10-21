using ExistenceEnum = Lifestyles.Domain.Live.Entities.Existence;

namespace Lifestyles.Api.Live.Map
{
    public static class Existence
    {
        public static ExistenceEnum Map(this string existence)
        {
            switch (existence)
            {
                case "expected": return ExistenceEnum.Expected;
                case "suggested": return ExistenceEnum.Suggested;
                default: return ExistenceEnum.Excluded;
            }
        }
    }
}
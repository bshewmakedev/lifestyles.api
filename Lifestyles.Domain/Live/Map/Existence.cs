using Lifestyles.Domain.Live.Constants;
using ExistenceEnum = Lifestyles.Domain.Live.Constants.Existence;

namespace Lifestyles.Domain.Live.Map
{
    public static class Existence
    {
        public static ExistenceEnum Map(this string existence)
        {
            switch (existence)
            {
                case ExistenceAlias.Expected: return ExistenceEnum.Expected;
                case ExistenceAlias.Suggested: return ExistenceEnum.Suggested;
                default: return ExistenceEnum.Excluded;
            }
        }

        public static string Map(this ExistenceEnum existence)
        {
            switch (existence)
            {
                case ExistenceEnum.Expected: return ExistenceAlias.Expected;
                case ExistenceEnum.Suggested: return ExistenceAlias.Suggested;
                default: return ExistenceAlias.Excluded;
            }
        }
    }
}
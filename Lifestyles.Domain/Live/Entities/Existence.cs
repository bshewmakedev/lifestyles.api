namespace Lifestyles.Domain.Live.Entities
{
    public enum Existence
    {
        Excluded, // by the user
        Expected, // by the user
        Suggested // by the service
    }

    public class ExistenceAlias
    {
        public const string Excluded = "excluded";
        public const string Expected = "expected";
        public const string Suggested = "suggested";
    }
}
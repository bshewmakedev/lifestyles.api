namespace Lifestyles.Domain.Live.Constants
{
    public enum Recurrence
    {
        Never,
        Daily,
        Weekly,
        Monthly,
        Annually
    }

    public static class RecurrenceAlias
    {
        public const string Never = "never";
        public const string Daily = "daily";
        public const string Weekly = "weekly";
        public const string Monthly = "monthly";
        public const string Annually = "annually";
    }
}
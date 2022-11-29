using RecurrenceEnum = Lifestyles.Domain.Live.Entities.Recurrence;

namespace Lifestyles.Domain.Live.Map
{
    public static class Recurrence
    {
        public const string Daily = "daily";
        public const string Weekly = "weekly";
        public const string Monthly = "monthly";
        public const string Annually = "annually";
        public const string Never = "never";

        public static RecurrenceEnum Map(this string recurrence)
        {
            switch (recurrence)
            {
                case Daily: return RecurrenceEnum.Daily;
                case Weekly: return RecurrenceEnum.Weekly;
                case Monthly: return RecurrenceEnum.Monthly;
                case Annually: return RecurrenceEnum.Annually;
                default: return RecurrenceEnum.Never;
            }
        }

        public static string Map(this RecurrenceEnum recurrence)
        {
            switch (recurrence)
            {
                case RecurrenceEnum.Daily: return Daily;
                case RecurrenceEnum.Weekly: return Weekly;
                case RecurrenceEnum.Monthly: return Monthly;
                case RecurrenceEnum.Annually: return Annually;
                default: return Never;
            }
        }
    }
}
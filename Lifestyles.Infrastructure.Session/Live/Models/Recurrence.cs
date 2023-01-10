using RecurrenceEnum = Lifestyles.Domain.Live.Entities.Recurrence;

namespace Lifestyles.Infrastructure.Session.Live.Models
{
    public static class Recurrence
    {
        public const string Daily = "daily";
        public const string Weekly = "weekly";
        public const string Monthly = "monthly";
        public const string Annually = "annually";
        public const string Never = "never";

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
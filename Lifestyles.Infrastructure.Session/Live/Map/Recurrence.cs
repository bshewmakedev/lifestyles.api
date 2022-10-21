using RecurrenceEnum = Lifestyles.Domain.Live.Entities.Recurrence;

namespace Lifestyles.Infrastructure.Session.Live.Map
{
    public static class Recurrence
    {
        public static RecurrenceEnum Map(this string recurrence)
        {
            switch (recurrence)
            {
                case "daily": return RecurrenceEnum.Daily;
                case "weekly": return RecurrenceEnum.Weekly;
                case "monthly": return RecurrenceEnum.Monthly;
                case "annually": return RecurrenceEnum.Annually;
                default: return RecurrenceEnum.Never;
            }
        }
    }
}
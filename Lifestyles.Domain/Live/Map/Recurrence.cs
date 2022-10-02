using Lifestyles.Domain.Live.Constants;
using RecurrenceEnum = Lifestyles.Domain.Live.Constants.Recurrence;

namespace Lifestyles.Domain.Live.Map
{
    public static class Recurrence
    {
        public static RecurrenceEnum Map(this string recurrence)
        {
            switch (recurrence)
            {
                case RecurrenceAlias.Daily: return RecurrenceEnum.Daily;
                case RecurrenceAlias.Weekly: return RecurrenceEnum.Weekly;
                case RecurrenceAlias.Monthly: return RecurrenceEnum.Monthly;
                case RecurrenceAlias.Annually: return RecurrenceEnum.Annually;
                default: return RecurrenceEnum.Never;
            }
        }

        public static int Map(this RecurrenceEnum recurrence)
        {
            switch (recurrence)
            {
                case RecurrenceEnum.Daily: return 1;
                case RecurrenceEnum.Weekly: return 7;
                case RecurrenceEnum.Monthly: return 31;
                case RecurrenceEnum.Annually: return 366;
                default: return 0;
            }
        }
    }
}
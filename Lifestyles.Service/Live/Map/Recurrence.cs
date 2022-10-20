using Lifestyles.Domain.Live.Entities;
using RecurrenceEnum = Lifestyles.Domain.Live.Entities.Recurrence;

namespace Lifestyles.Service.Live.Map
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

        public static string Map(this RecurrenceEnum recurrence)
        {
            switch (recurrence)
            {
                case RecurrenceEnum.Daily: return RecurrenceAlias.Daily;
                case RecurrenceEnum.Weekly: return RecurrenceAlias.Weekly;
                case RecurrenceEnum.Monthly: return RecurrenceAlias.Monthly;
                case RecurrenceEnum.Annually: return RecurrenceAlias.Annually;
                default: return RecurrenceAlias.Never;
            }
        }
    }
}
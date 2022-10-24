using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Service.Live.Models
{
    public static class DefaultRecurrence
    {
        public static string Map(this Recurrence recurrence)
        {
            switch (recurrence)
            {
                case Recurrence.Daily: return "daily";
                case Recurrence.Weekly: return "weekly";
                case Recurrence.Monthly: return "monthly";
                case Recurrence.Annually: return "annually";
                default: return "never";
            }
        }
    }
}
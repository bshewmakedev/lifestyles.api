using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Api.Live.Models
{
    public static class VmRecurrence
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
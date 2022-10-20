using Lifestyles.Api.Categorize.Models;
using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Api.Live.Models
{
    public class VmLifestyle : VmCategory
    {
        public int? Lifetime { get; set; }
        public string Recurrence { get; set; }
        public string Existence { get; set; }

        public VmLifestyle(ILifestyle lifestyle) : base(lifestyle)
        {
            Lifetime = lifestyle.Lifetime;
            Recurrence = Lifestyles.Service.Live.Map.Recurrence.Map(lifestyle.Recurrence);
            Existence = Lifestyles.Service.Live.Map.Existence.Map(lifestyle.Existence);
        }
    }
}
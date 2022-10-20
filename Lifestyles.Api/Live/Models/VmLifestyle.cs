using Lifestyles.Api.Categorize.Models;
using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Api.Live.Models
{
    public class VmLifestyle : VmCategory
    {
        public int? Lifetime { get; set; }
        public string Recurrence { get; set; } = "never";
        public string Existence { get; set; } = "expected";

        public VmLifestyle() {}

        public VmLifestyle(ILifestyle lifestyle) : base(lifestyle)
        {
            Lifetime = lifestyle.Lifetime;
            Recurrence = Lifestyles.Service.Live.Map.Recurrence.Map(lifestyle.Recurrence);
            Existence = Lifestyles.Service.Live.Map.Existence.Map(lifestyle.Existence);
        }
    }
}
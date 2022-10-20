using Lifestyles.Api.Live.Models;
using LifestyleMap = Lifestyles.Service.Live.Map.Lifestyle;

namespace Lifestyles.Api.Live.Map
{
    public class Lifestyle : LifestyleMap
    {
        public Lifestyle(VmLifestyle vmLifestyle) : base(
            vmLifestyle.Id,
            vmLifestyle.Label,
            vmLifestyle.Lifetime,
            Lifestyles.Service.Live.Map.Recurrence.Map(vmLifestyle.Recurrence),
            Lifestyles.Service.Live.Map.Existence.Map(vmLifestyle.Existence))
        { }
    }
}
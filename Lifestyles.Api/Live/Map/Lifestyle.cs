using Lifestyles.Api.Live.Models;
using LifestyleMap = Lifestyles.Service.Live.Map.Lifestyle;

namespace Lifestyles.Api.Budget.Map
{
    public class Lifestyle : LifestyleMap
    {
        public Lifestyle(VmLifestyle vmLifestyle) : base(
            vmLifestyle.Id,
            vmLifestyle.Label,
            Lifestyles.Domain.Live.Map.Recurrence.Map(vmLifestyle.Recurrence),
            Lifestyles.Domain.Live.Map.Existence.Map(vmLifestyle.Existence))
        { }
    }
}
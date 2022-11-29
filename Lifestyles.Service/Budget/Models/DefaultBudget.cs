using Lifestyles.Service.Live.Models;

namespace Lifestyles.Service.Budget.Models
{
    public class DefaultBudget : DefaultLifestyle
    {
        public decimal Value { get; set; }

        public DefaultBudget() { }
    }
}
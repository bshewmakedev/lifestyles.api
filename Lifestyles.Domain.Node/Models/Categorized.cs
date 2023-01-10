using System;

namespace Lifestyles.Domain.Node.Models
{
    public interface ICategorized
    {
        Guid EntityId { get; set; }
        Guid CategoryId { get; set; }
    }

    public class Categorized : ICategorized
    {
        public Guid EntityId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
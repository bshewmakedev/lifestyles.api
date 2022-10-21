using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Infrastructure.Session.Categorize.Models
{
    public class JsonCategorize
    {
        public Guid EntityId { get; set; }
        public Guid CategoryId { get; set; }

        public JsonCategorize() { }

        public JsonCategorize(
            IIdentified entity, 
            Guid categoryId)
        {
            EntityId = entity.Id;
            CategoryId = categoryId;
        }
    }
}
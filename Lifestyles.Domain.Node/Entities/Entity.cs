using System;

namespace Lifestyles.Domain.Node.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        string Alias { get; set; }
        string Label { get; set; }

        IEntity Identify(Guid? id = null);
        IEntity As(string alias = "");
        IEntity Relabel(string label = "");
    }

    public class Entity : IEntity
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }
        public string Label { get; set; }

        public Entity() { }

        public Entity(
            Guid? id = null,
            string alias = "",
            string label = "")
        {
            Identify(id);
            As(alias);
            Relabel(label);
        }

        public IEntity Identify(Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();

            return this;
        }

        public IEntity As(string alias = "")
        {
            Alias = alias;

            return this;
        }

        public IEntity Relabel(string label = "")
        {
            Label = label;

            return this;
        }
    }
}
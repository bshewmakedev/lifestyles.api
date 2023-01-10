using System.Collections.Generic;

namespace Lifestyles.Domain.Node.Entities
{
    public interface IGrouped<T>
    {
        T AsEntity { get; set; }
        IList<T> Entities { get; set; }
    }

    public class Grouped<T> : IGrouped<T>
    {
        public T AsEntity { get; set; }
        public IList<T> Entities { get; set; } = new List<T>();
    }
}
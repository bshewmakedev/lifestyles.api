using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Service.Live.Map
{
    public class Node<T> : INode<T>
        where T : IIdentified
    {
        public T Value { get; private set; }
        public INode<T>? Parent { get; private set; }
        public IList<INode<T>> Children { get; private set; } = new List<INode<T>>();

        public Node(T value)
        {
            Value = value;
        }

        public INode<T> AddValueAsChild(T value)
        {
            var node = new Node<T>(value) { Parent = this };
            Children.Add(node);

            return node;
        }

        public IList<INode<T>> AddValuesAsChildren(params T[] values)
        {
            return values.Select(AddValueAsChild).ToArray();
        }

        public bool RemoveChild(INode<T> node)
        {
            return Children.Remove(node);
        }

        // TODO : Unit test this method.
        public void Traverse(Action<T> action)
        {
            action(Value);
            foreach (var child in Children)
                child.Traverse(action);
        }

        // TODO : Unit test this method.
        public IEnumerable<T> FlattenValues()
        {
            return new[] { Value }.Concat(Children.SelectMany(x => x.FlattenValues()));
        }
    }
}
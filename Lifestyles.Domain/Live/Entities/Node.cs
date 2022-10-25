namespace Lifestyles.Domain.Live.Entities
{
    public class Node<T>
    {
        public T Value { get; private set; }
        public IList<Node<T>> Children { get; private set; }

        public Node(T value)
        {
            Value = value;
            Children = new List<Node<T>>();
        }

        public Node<T> AddNodeAsChild(Node<T> node)
        {
            Children.Add(node);

            return node;
        }

        public Node<T> AddValueAsChild(T value)
        {
            var node = new Node<T>(value);
            Children.Add(node);

            return node;
        }

        public IList<Node<T>> AddValuesAsChildren(params T[] values)
        {
            return values.Select(AddValueAsChild).ToList();
        }

        public bool RemoveChild(Node<T> node)
        {
            return Children.Remove(node);
        }

        public void Traverse(Action<T> action)
        {
            action(Value);
            foreach (var child in Children)
                child.Traverse(action);
        }

        public Node<G> Map<G>(Func<T, G> map)
        {
            var root = new Node<G>(map(Value));
            root.Children = Children.Select(child => child.Map<G>(map)).ToList();
            return root;
        }

        public IEnumerable<T> FlattenValues()
        {
            return new[] { Value }.Concat(Children.SelectMany(x => x.FlattenValues()));
        }
    }
}
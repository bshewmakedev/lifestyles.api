namespace Lifestyles.Domain.Tree.Entities
{
    public class Node<T>
    {
        public T Entity { get; private set; }
        public Node<T> Parent { get; private set; }
        public IList<Node<T>> Children { get; private set; }

        public Node(T entity)
        {
            Entity = entity;
            Children = new List<Node<T>>();
        }

        public Node<T> AddNodeAsChild(Node<T> node)
        {
            node.Parent = this;
            Children.Add(node);

            return node;
        }

        public IEnumerable<Node<T>> AddNodesAsChildren(params Node<T>[] nodes)
        {
            return nodes.Select(AddNodeAsChild).ToList();
        }

        public Node<T> AddEntityAsChild(T entity)
        {
            return AddNodeAsChild(new Node<T>(entity));
        }

        public IList<Node<T>> AddValuesAsChildren(params T[] entities)
        {
            return entities.Select(AddEntityAsChild).ToList();
        }

        public bool RemoveChild(Node<T> node)
        {
            return Children.Remove(node);
        }

        public void Traverse(Action<T> action)
        {
            action(Entity);
            foreach (var child in Children)
                child.Traverse(action);
        }

        public Node<G> Map<G>(Func<T, G> map)
        {
            var root = new Node<G>(map(Entity));
            root.Children = Children.Select(child => child.Map<G>(map)).ToList();
            return root;
        }

        public IEnumerable<T> FlattenValues()
        {
            return new[] { Entity }.Concat(Children.SelectMany(x => x.FlattenValues()));
        }
    }
}
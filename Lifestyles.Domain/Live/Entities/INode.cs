namespace Lifestyles.Domain.Live.Entities
{
    public interface INode<T>
    {
        T Value { get; }
        IList<INode<T>> Children { get; }
        INode<T> AddValueAsChild(T value);
        IList<INode<T>> AddValuesAsChildren(params T[] values);
        bool RemoveChild(INode<T> node);
        void Traverse(Action<T> action);
        INode<G> Map<G>(Func<T, G> map);
        IEnumerable<T> FlattenValues();
    }
}
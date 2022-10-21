namespace Lifestyles.Domain.Live.Entities
{
    public interface INode<T>
    {
        T Value { get; }
        INode<T>? Parent { get; }
        IList<INode<T>> Children { get; }
        INode<T> AddValueAsChild(T value);
        IList<INode<T>> AddValuesAsChildren(params T[] values);
        bool RemoveChild(INode<T> node);
        void Traverse(Action<T> action);
        IEnumerable<T> FlattenValues();
    }
}
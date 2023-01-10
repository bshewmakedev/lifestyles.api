using System.Collections.Generic;
using System;
using System.Linq;

namespace Lifestyles.Domain.Node.Entities
{
    public interface INode<T>
    {
        T Entity { get; }
        IList<INode<T>> Children { get; }

        INode<T> AddChildren(IList<INode<T>> nodes);
        INode<T> AddChildren(IList<T> entities);

        INode<T> RemoveChildren(IList<INode<T>> nodes);
        INode<T> RemoveChildren(IList<T> entities);

        INode<TResult> SelectDownRecursive<TResult>(Func<T, TResult> selector) where TResult : new();
        IList<T> FlattenDownRecursive();
        void ForEachUpRecursive(Action<T> action);
    }

    public class Node<T> : INode<T> where T : new()
    {
        public T Entity { get; private set; }
        public IList<INode<T>> Children { get; private set; }

        public Node(T entity)
        {
            Entity = entity;
            Children = new List<INode<T>>();
        }

        public INode<T> AddChildren(IList<INode<T>> nodes)
        {
            nodes.ToList().ForEach(n => Children.Add(n));

            return this;
        }

        public INode<T> AddChildren(IList<T> entities)
        {
            AddChildren(entities
                .Select(e => new Node<T>(e) as INode<T>)
                .ToList());

            return this;
        }

        public INode<T> RemoveChildren(IList<INode<T>> nodes)
        {
            nodes.ToList().ForEach(n => Children.Remove(n));

            return this;
        }

        public INode<T> RemoveChildren(IList<T> entities)
        {
            RemoveChildren(entities
                .Select(e => Children.FirstOrDefault(c => c.Entity.Equals(e)))
                .Where(n => n != null)
                .Select(n => n ?? new Node<T>(new T()))
                .ToList());

            return this;
        }

        public INode<TResult> SelectDownRecursive<TResult>(Func<T, TResult> selector) where TResult : new()
        {
            var root = new Node<TResult>(selector(Entity));
            root.AddChildren(Children
                .Select(c => c.SelectDownRecursive<TResult>(selector))
                .ToList());

            return root;
        }

        // public INode<T>? Find(T entity)
        // {
        //     if (entity.Equals(Entity))
        //     {
        //         return this;
        //     }

        //     foreach (var child in Children)
        //     {
        //         var nodeFound = child.Find(entity);
        //         if (nodeFound != null)
        //         {
        //             return nodeFound;
        //         }
        //     }

        //     return null;
        // }

        public void ForEachUpRecursive(Action<T> action)
        {
            action(Entity);
            foreach (var child in Children)
                child.ForEachUpRecursive(action);
        }

        public IList<T> FlattenDownRecursive()
        {
            return new[] { Entity }
                .Concat(Children.SelectMany(c => c.FlattenDownRecursive()))
                .ToList();
        }
    }
}
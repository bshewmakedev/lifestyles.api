using Lifestyles.Domain.Node.Entities;
using System.Collections.Generic;
using System.Linq;
using Lifestyles.Domain.Node.Models;

namespace Lifestyles.Domain.Node.Repositories
{
    public interface INodeRepo<T> where T : IEntity, new()
    {
        INode<T> Find();
        INode<T> FindGroupedAs(T entity);
        IList<T> Upsert(IList<T> entities);
        void Delete(IList<T> entities);
        IList<INode<T>> Group(IList<IGrouped<T>> groupings);
    }

    public static class NodeExtensions
    {
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> entities) where T : IEntity
        {
            return entities.Where(e => e != null);
        }

        public static IEnumerable<ICategorized> FindAllCategorizing<T>(
            this IEnumerable<ICategorized> categorized,
            T entity) where T : IEntity, new()
        {
            return categorized.Where(c => c.EntityId.Equals(entity.Id));
        }

        public static IEnumerable<ICategorized> FindAllCategorizedAs<T>(
            this IEnumerable<ICategorized> categorized,
            T asEntity) where T : IEntity, new()
        {
            return categorized.Where(c => c.CategoryId.Equals(asEntity.Id));
        }

        public static IEnumerable<T> FindUncategorized<T>(
            this IEnumerable<T> entities,
            IEnumerable<ICategorized> categorized) where T : IEntity, new()
        {
            return entities.Where(e => !categorized.FindAllCategorizing(e).Any()).ToList();
        }

        public static T EntityFor<T>(
            this IEnumerable<T> entities,
            ICategorized categorized) where T : IEntity, new()
        {
            return entities.FirstOrDefault(e => e.Id.Equals(categorized.EntityId));
        }

        public static T EntityFor<T>(
            this IEnumerable<T> entities,
            T entity) where T : IEntity, new()
        {
            return entities.FirstOrDefault(e => e.Id.Equals(entity.Id));
        }

        public static void Decategorize<T>(
            this IList<ICategorized> categorized,
            T entity) where T : IEntity, new()
        {
            categorized.FindAllCategorizing(entity)
                .ToList()
                .ForEach(c => categorized.Remove(c));
        }

        public static void DecategorizeAllAs<T>(
            this IList<ICategorized> categorized,
            T asEntity) where T : IEntity, new()
        {
            categorized.FindAllCategorizedAs(asEntity)
                .ToList()
                .ForEach(c => categorized.Remove(c));
        }

        public static void CategorizeAs<T>(
            this IList<ICategorized> categorized,
            T asEntity,
            T entity) where T : IEntity, new()
        {
            categorized.Add(new Categorized
            {
                EntityId = entity.Id,
                CategoryId = asEntity.Id
            });
        }

        public static T CoalesceByAlias<T>(
            this IEnumerable<T> entities,
            T entity) where T : IEntity, new()
        {
            if (!string.IsNullOrWhiteSpace(entity.Alias))
            {
                entity = entities.FirstOrDefault(e => e.Alias.Equals(entity.Alias)) ?? entity;
            }

            return entity;
        }
    }

    public class NodeRepo<T> : INodeRepo<T> where T : IEntity, new()
    {
        private static IList<T> _entities { get; set; } = new List<T>();
        private static IList<ICategorized> _categorized { get; set; } = new List<ICategorized>();

        // Finds all root entities.
        public INode<T> Find()
        {
            var entity = new T();
            entity.Identify().As().Relabel();

            var root = new Node<T>(entity);
            root.AddChildren(
                _entities.FindUncategorized(_categorized)
                .ToList());

            return root;
        }

        // Finds entities grouped as a given entity, along with their groups.
        public INode<T> FindGroupedAs(T entity)
        {
            entity = _entities.CoalesceByAlias(entity);
            var root = new Node<T>(entity);

            var children = _categorized.FindAllCategorizedAs(entity)
                .Select(c => _entities.EntityFor(c))
                .ToList()
                .Select(e => FindGroupedAs(e))
                .ToList();

            root.AddChildren(children);

            return root;
        }

        public IList<T> Upsert(IList<T> entities)
        {
            entities
                .ToList()
                .ForEach(entity =>
                {
                    var entityExisting = _entities.FirstOrDefault(e => e.Id.Equals(entity.Id));

                    if (entityExisting != null)
                    {
                        _entities.Remove(entityExisting);
                    }

                    _entities.Add(entity);
                });

            return entities
                .Select(entity => _entities.FirstOrDefault(e => e.Id.Equals(entity.Id)))
                .ToList();
        }

        // For each entity, delete subtress and then the entity.
        public void Delete(IList<T> entities)
        {
            entities
                .Select(entity => FindGroupedAs(entity))
                .ToList()
                .ForEach(node =>
                {
                    node.ForEachUpRecursive(entity =>
                    {
                        _categorized.DecategorizeAllAs(entity);
                        _categorized.Decategorize(entity);
                        _entities.Remove(_entities.EntityFor(entity));
                    });

                    _entities.Remove(_entities.EntityFor(node.Entity));
                });
        }

        public IList<INode<T>> Group(IList<IGrouped<T>> groupings)
        {
            return groupings
                .Select(grouping =>
                {
                    grouping.Entities.ToList().ForEach(entity =>
                    {
                        _categorized.Decategorize(entity);
                        _categorized.CategorizeAs(grouping.AsEntity, entity);
                    });

                    return FindGroupedAs(grouping.AsEntity);
                })
                .ToList();
        }
    }
}
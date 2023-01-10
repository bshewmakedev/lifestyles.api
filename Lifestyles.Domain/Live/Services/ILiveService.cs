using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Node.Entities;
using System.Collections.Generic;

namespace Lifestyles.Domain.Live.Services
{
    public interface ILiveService<T> where T : IBudget
    {
        INode<T> Find();
        INode<T> FindGroupedAs(T entity);
        IList<T> Upsert(IList<T> entities);
        void Delete(IList<T> entities);
        IList<INode<T>> Group(IList<IGrouped<T>> groupings);
    }
}
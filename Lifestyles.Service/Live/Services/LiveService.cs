using System.Collections.Generic;
using Lifestyles.Domain.Live.Services;
using Lifestyles.Domain.Node.Entities;
using Lifestyles.Domain.Node.Repositories;

namespace Lifestyles.Service.Live.Services
{
    public class LiveService<T> : ILiveService<T> where T : IEntity, new()
    {
        private readonly INodeRepo<T> _nodeRepo;

        public LiveService(INodeRepo<T> nodeRepo)
        {
            _nodeRepo = nodeRepo;
        }

        public INode<T> Find()
        {
            return _nodeRepo.Find();
        }

        public INode<T> FindGroupedAs(T entity)
        {
            return _nodeRepo.FindGroupedAs(entity);
        }

        public IList<T> Upsert(IList<T> entities)
        {
            return _nodeRepo.Upsert(entities);
        }

        public void Delete(IList<T> entities)
        {
            _nodeRepo.Delete(entities);
        }

        public IList<INode<T>> Group(IList<IGrouped<T>> groupings)
        {
            return _nodeRepo.Group(groupings);
        }
    }
}
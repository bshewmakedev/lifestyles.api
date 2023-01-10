using System.Collections.Generic;
using Lifestyles.Domain.Live.Services;
using Lifestyles.Domain.Node.Entities;
using Lifestyles.Domain.Node.Repositories;
using Newtonsoft.Json;

namespace Lifestyles.Service.Live.Services
{
    public class LiveService : ILiveService<Lifestyles.Service.Budget.Map.Budget>
    {
        private readonly INodeRepo<Lifestyles.Service.Budget.Map.Budget> _nodeRepo;

        public LiveService(INodeRepo<Lifestyles.Service.Budget.Map.Budget> nodeRepo)
        {
            _nodeRepo = nodeRepo;
        }

        public INode<Lifestyles.Service.Budget.Map.Budget> Find()
        {
            return _nodeRepo.Find();
        }

        public INode<Lifestyles.Service.Budget.Map.Budget> FindGroupedAs(Lifestyles.Service.Budget.Map.Budget entity)
        {
            return _nodeRepo.FindGroupedAs(entity);
        }

        public IList<Lifestyles.Service.Budget.Map.Budget> Upsert(IList<Lifestyles.Service.Budget.Map.Budget> entities)
        {
            return _nodeRepo.Upsert(entities);
        }

        public void Delete(IList<Lifestyles.Service.Budget.Map.Budget> entities)
        {
            _nodeRepo.Delete(entities);
        }

        public IList<INode<Lifestyles.Service.Budget.Map.Budget>> Group(IList<IGrouped<Lifestyles.Service.Budget.Map.Budget>> groupings)
        {
            return _nodeRepo.Group(groupings);
        }
    }
}
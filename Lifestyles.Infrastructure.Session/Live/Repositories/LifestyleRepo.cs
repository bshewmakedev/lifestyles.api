using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Repositories;
using LifestyleMap = Lifestyles.Infrastructure.Session.Live.Map.Lifestyle;

namespace Lifestyles.Infrastructure.Session.Live.Repositories
{
    public class LifestyleRepo : EntityRepo<ILifestyle, LifestyleMap>, ILifestyleRepo
    {
        public LifestyleRepo(IKeyValueRepo keyValueRepo) : base(
            keyValueRepo,
            Lifestyles.Domain.Tree.Map.NodeType.Lifestyle) { }
    }
}
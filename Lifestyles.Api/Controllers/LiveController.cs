using Microsoft.AspNetCore.Mvc;
using Lifestyles.Api.Budget.Models;
using Lifestyles.Api.Live.Models;
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Services;
using VmBudgetMap = Lifestyles.Api.Budget.Models.VmBudget;
using LifestyleMap = Lifestyles.Api.Live.Map.Lifestyle;

namespace Lifestyles.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LiveController : ControllerBase
{
    private readonly ILogger<BudgetController> _logger;
    private readonly ILiveService _liveService;

    public LiveController(
        ILogger<BudgetController> logger,
        ILiveService liveService)
    {
        _logger = logger;
        _liveService = liveService;
    }

    [HttpGet]
    [Route("recurrences/find")]
    public IEnumerable<string> FindRecurrences()
    {
        return _liveService
            .FindRecurrences()
            .Select(r => r.Map());
    }

    [HttpGet]
    [Route("existences/find")]
    public IEnumerable<string> FindExistences()
    {
        return _liveService
            .FindExistences()
            .Select(e => e.Map());
    }

    [HttpGet]
    [Route("lifetrees/find/default")]
    public IEnumerable<INode<VmBudget>> FindDefaultLifeTrees()
    {
        return _liveService
            .FindDefaultLifeTrees()
            .Select(tree => tree.Map<VmBudget>((budget) => new VmBudgetMap(budget)));
    }

    [HttpGet]
    [Route("lifetrees/find")]
    public IEnumerable<INode<VmBudget>> FindSavedLifeTrees()
    {
        return _liveService
            .FindSavedLifeTrees()
            .Select(tree => tree.Map<VmBudget>((budget) => new VmBudgetMap(budget)));
    }

    // [HttpPost]
    // [Route("lifetrees/upsert")]
    // public IEnumerable<INode<IBudget>> UpsertSavedLifeTrees(List<Node<VmBudget>> lifeTrees)
    // {
    //     return _liveService
    //         .UpsertSavedLifeTrees(lifeTrees);
    // }
    
    [HttpPost]
    [Route("lifestyles/compare")]
    public IEnumerable<VmComparison<VmLifestyle, ILifestyle>> CompareLifestyles(List<VmLifestyle> vmLifestyles)
    {
        return _liveService
            .CompareLifestyles(vmLifestyles.Select(l => new LifestyleMap(l)))
            .Select(l => new VmComparison<VmLifestyle, ILifestyle>(l));
    }
}

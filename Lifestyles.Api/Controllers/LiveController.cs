using Microsoft.AspNetCore.Mvc;
using Lifestyles.Infrastructure.Session.Budget.Models;
using Lifestyles.Infrastructure.Session.Live.Models;
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Services;
using Lifestyles.Service.Live.Map;
using BudgetMap = Lifestyles.Infrastructure.Session.Budget.Map.Budget;
using CategoryMap = Lifestyles.Infrastructure.Session.Categorize.Map.Category;
using LifestyleMap = Lifestyles.Infrastructure.Session.Live.Map.Lifestyle;

namespace Lifestyles.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LiveController : ControllerBase
{
    private readonly ILogger<LiveController> _logger;
    private readonly ILiveService _liveService;

    public LiveController(
        ILogger<LiveController> logger,
        ILiveService liveService)
    {
        _logger = logger;
        _liveService = liveService;
    }

    /// <summary>
    /// Find valid recurrences with which to describe { lifestyles, budgets }.
    /// </summary>
    /// <returns>aliases for recurrences</returns>
    [HttpGet]
    [Route("recurrences/find")]
    public IEnumerable<string> FindRecurrences()
    {
        return _liveService
            .FindRecurrences()
            .Select(r => r.Map());
    }

    /// <summary>
    /// Find valid existences with which to describe { lifestyles, budgets }.
    /// </summary>
    /// <returns>aliases for existences</returns>
    [HttpGet]
    [Route("existences/find")]
    public IEnumerable<string> FindExistences()
    {
        return _liveService
            .FindExistences()
            .Select(e => e.Map());
    }

    /// <summary>
    /// Find default lifestyles with their categorized budgets.
    /// </summary>
    /// <returns>trees that represent default lifestyles</returns>
    [HttpGet]
    [Route("lifetrees/find/default")]
    public IEnumerable<Node<JsonBudget>> FindDefaultLifeTrees()
    {
        return _liveService
            .FindDefaultLifeTrees()
            .Select(tree => tree.Map<JsonBudget>((budget) => new JsonBudget(budget)));
    }

    /// <summary>
    /// Find saved { lifestyles, categories } with their {categories, budgets }.
    /// </summary>
    /// <param name="lifestyleIds">optional filter by lifestyles' IDs</param>
    /// <returns>trees that represent saved lifestyles</returns>
    [HttpGet]
    [Route("lifetrees/find/{lifestyleIds?}")]
    public IEnumerable<Node<JsonBudget>> FindSavedLifeTrees([FromQuery] Guid[]? lifestyleIds = null)
    {
        return _liveService
            .FindSavedLifeTrees(lifestyleIds)
            .Select(tree => tree.Map<JsonBudget>((budget) => new JsonBudget(budget)));
    }

    /// <summary>
    /// Given a flat list of { lifestyles, categories, budgets },
    ///   - deassociate them with their existing parents
    ///   - associate   them with their new      parents
    ///   - insert them if they do not exist
    ///   - update them if they do     exist
    /// </summary>
    /// <param name="lifeTrees">flat list of { lifestyles, categories, budgets } to upsert</param>
    /// <returns>flat list of upserted { lifestyles, categories, budgets }</returns>
    [HttpPost]
    [Route("lifetrees/upsert")]
    public IEnumerable<Node<JsonBudget>> UpsertSavedLifeTrees(List<Node<JsonBudget>> lifeTrees)
    {
        return _liveService
            .UpsertSavedLifeTrees(lifeTrees.Select(tree => tree.Map<IBudget>((vmBudget) => new BudgetMap(vmBudget))))
            .Select(tree => tree.Map<JsonBudget>((budget) => new JsonBudget(budget)));
    }

    /// <summary>
    /// Given a flat list of { lifestyles, categories, budgets },
    ///   - remove their descendants
    ///   - remove them
    /// </summary>
    /// <param name="lifeTrees">flat list of { lifestyles, categories, budgets } to remove</param>
    /// <returns>flat list of removed { lifestyles, categories, budgets }</returns>
    [HttpPost]
    [Route("lifetrees/remove")]
    public IEnumerable<Node<JsonBudget>> RemoveSavedLifeTrees(List<Node<JsonBudget>> lifeTrees)
    {
        return _liveService
            .UpsertSavedLifeTrees(
                lifeTrees.Select(tree => tree.Map<IBudget>((vmBudget) => new BudgetMap(vmBudget))))
            .Select(tree => tree.Map<JsonBudget>((budget) => new JsonBudget(budget)));
    }

    /// <summary>
    /// Given a flat list of { lifestyles, categories, budgets },
    ///   - calculate their aggregate value
    ///   - rank them against one another
    /// </summary>
    /// <param name="vmLifestyles">flat list of { lifestyles, categories, budgets } to remove</param>
    /// <returns>flat list of removed { lifestyles, categories, budgets }</returns>
    [HttpPost]
    [Route("lifestyles/compare")]
    public IEnumerable<IComparison<JsonBudget>> CompareLifestyles(List<JsonBudget> vmLifestyles)
    {
        return new List<IComparison<JsonBudget>>();
    }
}

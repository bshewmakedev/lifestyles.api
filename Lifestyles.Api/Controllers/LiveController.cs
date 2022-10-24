using Microsoft.AspNetCore.Mvc;
using Lifestyles.Domain.Live.Services;
using Lifestyles.Api.Budget.Models;
using Lifestyles.Api.Live.Models;
using Lifestyles.Domain.Live.Entities;
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
    [Route("lifestyles/find")]
    public IEnumerable<VmLifestyle> FindLifestyles()
    {
        return _liveService
            .FindLifestyles()
            .Select(l => new VmLifestyle(l));
    }

    [HttpPost]
    [Route("lifestyles/upsert")]
    public IEnumerable<VmLifestyle> UpsertLifestyles(List<VmLifestyle> vmLifestyles)
    {
        return _liveService
            .UpsertLifestyles(vmLifestyles.Select(l => new LifestyleMap(l)))
            .Select(l => new VmLifestyle(l));
    }

    [HttpPost]
    [Route("lifestyles/remove")]
    public IEnumerable<VmLifestyle> RemoveLifestyles(List<VmLifestyle> vmLifestyles)
    {
        return _liveService
            .RemoveLifestyles(vmLifestyles.Select(l => new LifestyleMap(l)))
            .Select(l => new VmLifestyle(l));
    }

    [HttpPost]
    [Route("lifestyles/compare")]
    public IEnumerable<VmComparison<VmLifestyle, ILifestyle>> CompareLifestyles(List<VmLifestyle> vmLifestyles)
    {
        return _liveService
            .CompareLifestyles(vmLifestyles.Select(l => new LifestyleMap(l)))
            .Select(l => new VmComparison<VmLifestyle, ILifestyle>(l));
    }

    [HttpGet]
    [Route("amount/get/{lifestyleId}")]
    public decimal GetSignedAmount(Guid lifestyleId)
    {
        return _liveService.GetSignedAmount(lifestyleId);
    }
}

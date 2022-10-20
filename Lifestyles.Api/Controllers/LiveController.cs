using Microsoft.AspNetCore.Mvc;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Api.Live.Models;
using LifestyleMap = Lifestyles.Api.Live.Map.Lifestyle;
using RecurrenceMap = Lifestyles.Service.Live.Map.Recurrence;
using ExistenceMap = Lifestyles.Service.Live.Map.Existence;

namespace Lifestyles.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LiveController : ControllerBase
{
    private readonly ILogger<BudgetController> _logger;
    private readonly IKeyValueRepo _context;
    private readonly IRecurrenceRepo _recurrenceRepo;
    private readonly IExistenceRepo _existenceRepo;
    private readonly ILifestyleRepo _lifestyleRepo;

    public LiveController(
        ILogger<BudgetController> logger,
        IKeyValueRepo context,
        IRecurrenceRepo recurrenceRepo,
        IExistenceRepo existenceRepo,
        ILifestyleRepo lifestyleRepo)
    {
        _logger = logger;
        _context = context;
        _recurrenceRepo = recurrenceRepo;
        _existenceRepo = existenceRepo;
        _lifestyleRepo = lifestyleRepo;
    }

    [HttpGet]
    [Route("recurrences/find")]
    public IEnumerable<string> FindRecurrences()
    {
        return _recurrenceRepo.Find().Select(r => RecurrenceMap.Map(r));
    }

    [HttpGet]
    [Route("existences/find")]
    public IEnumerable<string> FindExistences()
    {
        return _existenceRepo.Find().Select(e => ExistenceMap.Map(e));
    }

    [HttpGet]
    [Route("lifestyles/find")]
    public IEnumerable<VmLifestyle> FindLifestyles()
    {
        return _lifestyleRepo.Find().Select(l => new VmLifestyle(l));
    }

    [HttpPost]
    [Route("lifestyles/upsert")]
    public IEnumerable<VmLifestyle> UpsertLifestyles(List<VmLifestyle> vmLifestyles)
    {
        return _lifestyleRepo
            .Upsert(vmLifestyles.Select(l => new LifestyleMap(l)))
            .Select(l => new VmLifestyle(l));
    }
    
    [HttpPost]
    [Route("lifestyles/remove")]
    public IEnumerable<VmLifestyle> RemoveLifestyles(List<VmLifestyle> vmLifestyles)
    {
        return _lifestyleRepo
            .Remove(vmLifestyles.Select(l => new LifestyleMap(l)))
            .Select(l => new VmLifestyle(l));
    }
}

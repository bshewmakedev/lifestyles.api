using Microsoft.AspNetCore.Mvc;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Api.Live.Models;
using Lifestyles.Domain.Live.Repositories;
using RecurrenceMap = Lifestyles.Domain.Live.Map.Recurrence;
using ExistenceMap = Lifestyles.Domain.Live.Map.Existence;

namespace Lifestyles.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LiveController : ControllerBase
{
    private readonly ILogger<BudgetController> _logger;
    private readonly IKeyValueRepo _context;
    private readonly IBudgetTypeRepo _budgetTypeRepo;
    private readonly IRecurrenceRepo _recurrenceRepo;
    private readonly IExistenceRepo _existenceRepo;
    private readonly IBudgetRepo _budgetRepo;
    private readonly ICategoryRepo _categoryRepo;
    private readonly ILifestyleRepo _lifestyleRepo;

    public LiveController(
        ILogger<BudgetController> logger,
        IKeyValueRepo context,
        IBudgetTypeRepo budgetTypeRepo,
        IRecurrenceRepo recurrenceRepo,
        IExistenceRepo existenceRepo,
        IBudgetRepo budgetRepo,
        ICategoryRepo categoryRepo,
        ILifestyleRepo lifestyleRepo)
    {
        _logger = logger;
        _context = context;
        _budgetTypeRepo = budgetTypeRepo;
        _recurrenceRepo = recurrenceRepo;
        _existenceRepo = existenceRepo;
        _budgetRepo = budgetRepo;
        _categoryRepo = categoryRepo;
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
        return _lifestyleRepo.Find().Select(b => new VmLifestyle(b));
    }

    [HttpGet]
    [Route("lifestyles/amount/{lifestyleId}")]
    public decimal CalculateAmount(Guid lifestyleId)
    {
        var lifestyle = _lifestyleRepo.Find().FirstOrDefault(l => l.Id.Equals(lifestyleId));

        var budgets = _budgetRepo.FindCategorizedAs(lifestyleId);
        
        return lifestyle.GetAmount(budgets);
    }

    [HttpGet]
    [Route("lifestyles/default")]
    public void Default()
    {
        _budgetTypeRepo.Default();
        _recurrenceRepo.Default();
        _existenceRepo.Default();
        _lifestyleRepo.Default();
        _categoryRepo.Default();
        _budgetRepo.Default();
    }
}

using Microsoft.AspNetCore.Mvc;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Database.Budget.Models;
using Lifestyles.Infrastructure.Database.Live.Models;

namespace Lifestyles.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LiveController : ControllerBase
{
    private readonly ILogger<BudgetController> _logger;
    private readonly IKeyValueStorage _context;
    private readonly IBudgetTypeRepo _budgetTypeRepo;
    private readonly IRecurrenceRepo _recurrenceRepo;
    private readonly IExistenceRepo _existenceRepo;
    private readonly IBudgetRepo _budgetRepo;
    private readonly ICategoryRepo _categoryRepo;
    private readonly ILifestyleRepo _lifestyleRepo;

    public LiveController(
        ILogger<BudgetController> logger,
        IKeyValueStorage context,
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
    [Route("lifestyles/find")]
    public IEnumerable<ILifestyle> Find()
    {
        return _lifestyleRepo.Find();
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

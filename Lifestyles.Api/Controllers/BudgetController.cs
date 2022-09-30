using Microsoft.AspNetCore.Mvc;
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Repositories;

namespace Lifestyles.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BudgetController : ControllerBase
{
    private readonly ILogger<BudgetController> _logger;
    private readonly IRepository<IBudget> _budgetRepo;

    public BudgetController(
        ILogger<BudgetController> logger,
        IRepository<IBudget> budgetRepo)
    {
        _budgetRepo = budgetRepo;
        _logger = logger;
    }

    [HttpGet]
    [Route("findall")]
    public IEnumerable<IBudget> Get()
    {
        return _budgetRepo.Find();
    }
}

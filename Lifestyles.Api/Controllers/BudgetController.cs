using Microsoft.AspNetCore.Mvc;
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;

namespace Lifestyles.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BudgetController : ControllerBase
{
    private readonly ILogger<BudgetController> _logger;
    private readonly IBudgetRepo _budgetRepo;

    public BudgetController(
        ILogger<BudgetController> logger,
        IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
        _logger = logger;
    }

    [HttpGet]
    [Route("budgets/find")]
    public IEnumerable<IBudget> Find()
    {
        return _budgetRepo.Find();
    }

    [HttpGet]
    [Route("budgets/find/{categoryId}")]
    public IEnumerable<IBudget> FindByCategoryId(Guid categoryId)
    {
        return _budgetRepo.FindCategorizedAs(categoryId);
    }
}

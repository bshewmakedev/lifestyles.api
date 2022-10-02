using Microsoft.AspNetCore.Mvc;
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;
using BudgetTypeMap = Lifestyles.Domain.Budget.Map.BudgetType;

namespace Lifestyles.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BudgetController : ControllerBase
{
    private readonly ILogger<BudgetController> _logger;
    private readonly IBudgetTypeRepo _budgetTypeRepo;
    private readonly IBudgetRepo _budgetRepo;

    public BudgetController(
        ILogger<BudgetController> logger,
        IBudgetTypeRepo budgetTypeRepo,
        IBudgetRepo budgetRepo)
    {
        _logger = logger;
        _budgetTypeRepo = budgetTypeRepo;
        _budgetRepo = budgetRepo;
    }

    [HttpGet]
    [Route("budgettypes/find")]
    public IEnumerable<string> FindBudgetTypes()
    {
        return _budgetTypeRepo.Find().Select(r => BudgetTypeMap.Map(r));
    }

    [HttpGet]
    [Route("budgets/find")]
    public IEnumerable<IBudget> FindBudgets()
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

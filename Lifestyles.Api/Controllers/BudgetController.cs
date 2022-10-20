using Microsoft.AspNetCore.Mvc;
using Lifestyles.Api.Budget.Models;
using Lifestyles.Domain.Budget.Repositories;
using BudgetTypeMap = Lifestyles.Service.Budget.Map.BudgetType;
using BudgetMap = Lifestyles.Api.Budget.Map.Budget;

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
    public IEnumerable<VmBudget> FindBudgets()
    {
        return _budgetRepo.Find().Select(b => new VmBudget(b));
    }

    [HttpGet]
    [Route("budgets/find/{categoryId}")]
    public IEnumerable<VmBudget> FindByCategoryId(Guid categoryId)
    {
        return _budgetRepo.FindCategorizedAs(categoryId).Select(b => new VmBudget(b));
    }

    [HttpPost]
    [Route("budgets/upsert/{categoryId}")]
    public IEnumerable<VmBudget> Upsert(List<VmBudget> vmBudgets)
    {
        return _budgetRepo
            .Upsert(vmBudgets.Select(b => new BudgetMap(b)))
            .Select(b => new VmBudget(b));
    }

    [HttpPost]
    [Route("budgets/remove")]
    public IEnumerable<VmBudget> Remove(List<VmBudget> vmBudgets)
    {
        return _budgetRepo
            .Remove(vmBudgets.Select(b => new BudgetMap(b)))
            .Select(b => new VmBudget(b));
    }
}

using Microsoft.AspNetCore.Mvc;
using Lifestyles.Api.Budget.Models;
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Services;
using BudgetMap = Lifestyles.Api.Budget.Map.Budget;

namespace Lifestyles.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BudgetController : ControllerBase
{
    private readonly ILogger<BudgetController> _logger;
    private readonly IBudgetService _budgetService;

    public BudgetController(
        ILogger<BudgetController> logger,
        IBudgetService budgetService)
    {
        _logger = logger;
        _budgetService = budgetService;
    }

    [HttpPost]
    [Route("budgets/compare")]
    public IEnumerable<VmComparison<VmBudget, IBudget>> CompareBudgets(List<VmBudget> vmBudgets)
    {
        return _budgetService
            .CompareBudgets(vmBudgets.Select(b => new BudgetMap(b)))
            .Select(b => new VmComparison<VmBudget, IBudget>(b));
    }
}

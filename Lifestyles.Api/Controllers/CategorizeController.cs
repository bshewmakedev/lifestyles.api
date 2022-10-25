using Microsoft.AspNetCore.Mvc;
using Lifestyles.Api.Categorize.Models;
using Lifestyles.Domain.Categorize.Services;
using CategoryMap = Lifestyles.Api.Categorize.Map.Category;

namespace Lifestyles.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategorizeController : ControllerBase
{
    private readonly ILogger<BudgetController> _logger;
    private readonly ICategorizeService _categorizeService;

    public CategorizeController(
        ILogger<BudgetController> logger,
        ICategorizeService categorizeService)
    {
        _logger = logger;
        _categorizeService = categorizeService;
    }
}

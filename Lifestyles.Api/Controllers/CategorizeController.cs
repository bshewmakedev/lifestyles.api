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

    [HttpGet]
    [Route("categories/find")]
    public IEnumerable<VmCategory> FindCategories()
    {
        return _categorizeService
            .FindCategories()
            .Select(c => new VmCategory(c));
    }

    [HttpGet]
    [Route("categories/find/{categoryId}")]
    public IEnumerable<VmCategory> FindCategoriesByCategoryId(Guid categoryId)
    {
        return _categorizeService
            .FindCategoriesByCategoryId(categoryId)
            .Select(c => new VmCategory(c));
    }

    [HttpPost]
    [Route("categories/categorize/{categoryId}")]
    public IEnumerable<VmCategory> CategorizeCategories(Guid categoryId, List<VmCategory> vmCategories)
    {
        return _categorizeService
            .CategorizeCategories(categoryId, vmCategories.Select(c => new CategoryMap(c)))
            .Select(b => new VmCategory(b));
    }

    [HttpPost]
    [Route("categories/upsert")]
    public IEnumerable<VmCategory> UpsertCategories(List<VmCategory> vmCategories)
    {
        return _categorizeService
            .UpsertCategories(vmCategories.Select(c => new CategoryMap(c)))
            .Select(c => new VmCategory(c));
    }

    [HttpPost]
    [Route("categories/remove")]
    public IEnumerable<VmCategory> RemoveCategories(List<VmCategory> vmCategories)
    {
        return _categorizeService
            .RemoveCategories(vmCategories.Select(c => new CategoryMap(c)))
            .Select(c => new VmCategory(c));
    }
}

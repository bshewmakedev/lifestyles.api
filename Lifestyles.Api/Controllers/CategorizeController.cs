using Microsoft.AspNetCore.Mvc;
using Lifestyles.Api.Categorize.Models;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Categorize.Repositories;
using CategoryMap = Lifestyles.Api.Categorize.Map.Category;

namespace Lifestyles.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategorizeController : ControllerBase
{
    private readonly ILogger<BudgetController> _logger;
    private readonly ICategoryRepo _categoryRepo;

    public CategorizeController(
        ILogger<BudgetController> logger,
        ICategoryRepo categoryRepo)
    {
        _logger = logger;
        _categoryRepo = categoryRepo;
    }

    [HttpGet]
    [Route("categories/find")]
    public IEnumerable<ICategory> Find()
    {
        return _categoryRepo.Find();
    }

    [HttpGet]
    [Route("categories/find/{lifestyleId}")]
    public IEnumerable<ICategory> FindByLifestyleId(Guid lifestyleId)
    {
        return _categoryRepo.FindCategorizedAs(lifestyleId);
    }

    [HttpPost]
    [Route("categories/upsert")]
    public IEnumerable<VmCategory> UpsertCategories(List<VmCategory> vmCategories)
    {
        return _categoryRepo
            .Upsert(vmCategories.Select(c => new CategoryMap(c)))
            .Select(c => new VmCategory(c));
    }

    [HttpPost]
    [Route("categories/remove")]
    public IEnumerable<VmCategory> RemoveCategories(List<VmCategory> vmCategories)
    {
        return _categoryRepo
            .Remove(vmCategories.Select(c => new CategoryMap(c)))
            .Select(c => new VmCategory(c));
    }
}

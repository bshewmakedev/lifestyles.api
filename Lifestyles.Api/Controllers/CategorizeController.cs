using Microsoft.AspNetCore.Mvc;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Categorize.Repositories;

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
        _categoryRepo = categoryRepo;
        _logger = logger;
    }

    [HttpGet]
    [Route("categories/find")]
    public IEnumerable<ICategory> Find()
    {
        return _categoryRepo.Find();
    }

    [HttpGet]
    [Route("categories/find/{categoryId}")]
    public IEnumerable<ICategory> FindByCategoryId(Guid categoryId)
    {
        return _categoryRepo.FindCategorizedAs(categoryId);
    }
}

using Microsoft.AspNetCore.Mvc;
using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Repositories;

namespace Lifestyles.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LiveController : ControllerBase
{
    private readonly ILogger<BudgetController> _logger;
    private readonly ILifestyleRepo _lifestyleRepo;

    public LiveController(
        ILogger<BudgetController> logger,
        ILifestyleRepo lifestyleRepo)
    {
        _lifestyleRepo = lifestyleRepo;
        _logger = logger;
    }

    [HttpGet]
    [Route("lifestyles/find")]
    public IEnumerable<IBudget> Get()
    {
        return _lifestyleRepo.Find();
    }
}
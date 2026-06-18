using ELearningPlatform.Contracts.Lerner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPlatform.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LearnersController(ILearnerService learnerService) : ControllerBase
{
    private readonly ILearnerService _learnerService = learnerService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _learnerService.GetAllAsync();
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _learnerService.GetAsync(id);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateLearner(CreateLearnerRequest request)
    {
        var result = await _learnerService.CreateAsync(request);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
}

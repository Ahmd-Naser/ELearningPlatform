using ELearningPlatform.Contracts.Enrollment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPlatform.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentsController(IEnrollmentService enrollmentService) : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService = enrollmentService;

    [HttpPost("")]
    public async Task<IActionResult> Enroll(EnrollmentRequest request)
    {
        var result = await _enrollmentService.AddAsync(request);

        return result.IsSuccess
            ? Ok()
            : result.ToProblem();
    }

    [HttpPost("{id}/decision")]
    public async Task<IActionResult> MakeDecision([FromRoute] int id, DecisionRequest request)
    {
        var result = await _enrollmentService.MakeDecisionAsync(id, request);

        return result.IsSuccess
            ? Ok()
            : result.ToProblem();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetEnrollments([FromQuery] EnrollmentFilterRequest filters)
    {
        var result = await _enrollmentService.GetEnrollmentsAsync(filters);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

}

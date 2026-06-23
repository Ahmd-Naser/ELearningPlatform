using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPlatform.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController(ICourseService courseService) : ControllerBase
{
    private readonly ICourseService _courseService = courseService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _courseService.GetAllAsync();
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _courseService.GetAsync(id);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateCourse(CreateCourseRequest request)
    {
        var result = await _courseService.CreateAsync(request);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateCourse([FromRoute] int id, [FromBody] UpdateCourseRequest request)
    {

        var result = await _courseService.UpdateAsync(id, request);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();

    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteCourse([FromRoute] int id)
    {
        var result = await _courseService.DeleteAsync(id);

        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }
}

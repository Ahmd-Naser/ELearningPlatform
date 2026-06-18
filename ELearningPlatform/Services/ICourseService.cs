
namespace ELearningPlatform.Services;

public interface ICourseService
{
    Task<Result<CourseResponse>> CreateAsync(CreateCourseRequest request);
    Task<Result<IEnumerable<CourseResponse>>> GetAllAsync();
    Task<Result<CourseResponse>> GetAsync(int id);
}

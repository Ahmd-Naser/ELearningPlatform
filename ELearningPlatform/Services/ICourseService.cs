
namespace ELearningPlatform.Services;

public interface ICourseService
{
    Task<Result<CourseResponse>> CreateAsync(CreateCourseRequest request);
    Task<Result<IEnumerable<CourseResponse>>> GetAllAsync();
    Task<Result<CourseResponse>> GetAsync(int id);
    Task<Result<CourseResponse>> UpdateAsync(int id, UpdateCourseRequest request);

    Task<Result> DeleteAsync(int id);
}

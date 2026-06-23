
namespace ELearningPlatform.Services;

public class CourseService(ApplicationDbContext context) : ICourseService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<CourseResponse>> CreateAsync(CreateCourseRequest request)
    {
        var course = request.Adapt<Course>();

        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();

        var response = course.Adapt<CourseResponse>();

        return Result.Success(response);
    }

    public async Task<Result<IEnumerable<CourseResponse>>> GetAllAsync()
    {
        var courses = await _context.Courses
            .ProjectToType<CourseResponse>()
            .AsNoTracking()
            .ToListAsync();

        return Result.Success<IEnumerable<CourseResponse>>(courses);
    }
        
    public async Task<Result<CourseResponse>> GetAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        return course is not null 
            ? Result.Success(course.Adapt<CourseResponse>())
            : Result.Failure<CourseResponse>(CourseErrors.NotFound);
    }

    public async Task<Result<CourseResponse>> UpdateAsync(int id, UpdateCourseRequest request)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course is null)
            return Result.Failure<CourseResponse>(CourseErrors.NotFound);

        request.Adapt(course);

        await _context.SaveChangesAsync();

        return Result.Success(course.Adapt<CourseResponse>());

    }

    public async Task<Result> DeleteAsync(int id)
    {

        var course = await _context.Courses.FindAsync(id);

        if (course is null)
            return Result.Failure(CourseErrors.NotFound);

        _context.Courses.Remove(course);

        await _context.SaveChangesAsync();

        return Result.Success();

    }
}

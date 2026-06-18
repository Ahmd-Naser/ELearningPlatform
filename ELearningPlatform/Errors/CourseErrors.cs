namespace ELearningPlatform.Errors;

public static class CourseErrors
{
    public static readonly Error NotFound =
        new("CourseErrors.NotFound", "No course was found with the given ID", StatusCodes.Status404NotFound);
    
    public static readonly Error Inactive =
        new("CourseErrors.Inactive", "This course is Inactive ", StatusCodes.Status400BadRequest);

}

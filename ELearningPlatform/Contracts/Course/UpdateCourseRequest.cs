namespace ELearningPlatform.Contracts.Course;

public record UpdateCourseRequest(

    string Title,

    string Description,

    int DurationHours,

    bool RequiresApproval,

    bool IsActive

);
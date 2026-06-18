namespace ELearningPlatform.Contracts.Course;

public record CreateCourseRequest(
    string Title,
    string Description,
    int DurationHours,
    bool RequiresApproval,
    bool IsActive
);
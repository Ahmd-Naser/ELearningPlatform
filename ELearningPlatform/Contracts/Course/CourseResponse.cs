namespace ELearningPlatform.Contracts.Course;

public record CourseResponse(
    string Title,
    string Description,
    int DurationHours,
    bool RequiresApproval,
    bool IsActive,
    DateTime CreatedAt
);
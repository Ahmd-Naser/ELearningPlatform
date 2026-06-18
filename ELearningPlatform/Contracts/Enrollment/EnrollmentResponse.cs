namespace ELearningPlatform.Contracts.Enrollment;

public record EnrollmentResponse(
    int Id,
    string Status,
    DateTime EnrollmentDate,
    LearnerBasicDto Learner,
    CourseBasicDto Course
);
public record LearnerBasicDto(int Id, string FullName, string Email);

public record CourseBasicDto(int Id, string Title);
namespace ELearningPlatform.Contracts.Enrollment;

public record EnrollmentRequest(
    int LearnerId,
    int CourseId
);
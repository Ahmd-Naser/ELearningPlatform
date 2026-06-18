namespace ELearningPlatform.Contracts.Enrollment;

public record EnrollmentFilterRequest(
    int? LearnerId,
    int? CourseId,
    string? Status,
    DateTime? FromDate,
    DateTime? ToDate
);
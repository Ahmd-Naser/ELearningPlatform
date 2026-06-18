namespace ELearningPlatform.Contracts.Lerner;

public record LearnerResponse(
    string FullName,
    string NationalId,
    string Email,
    string Department,
    DateTime CreatedAt
);

namespace ELearningPlatform.Contracts.Lerner;

public record CreateLearnerRequest(
    string FullName,
    string NationalId,
    string Email,
    string Department
);
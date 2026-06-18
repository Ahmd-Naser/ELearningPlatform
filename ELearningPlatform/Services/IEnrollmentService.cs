
using ELearningPlatform.Contracts.Enrollment;

namespace ELearningPlatform.Services;

public interface IEnrollmentService
{
    Task<Result> AddAsync(EnrollmentRequest request);
    Task<Result<List<EnrollmentResponse>>> GetEnrollmentsAsync(EnrollmentFilterRequest filters);
    Task<Result> MakeDecisionAsync(int id, DecisionRequest request);
}

using ELearningPlatform.Contracts.Lerner;

namespace ELearningPlatform.Services;

public interface ILearnerService
{
    Task<Result<LearnerResponse>> CreateAsync(CreateLearnerRequest request);
    Task<Result<IEnumerable<LearnerResponse>>> GetAllAsync();
    Task<Result<LearnerResponse>> GetAsync(int id);
}

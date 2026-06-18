using ELearningPlatform.Contracts.Lerner;
using Microsoft.AspNetCore.Components.Web;

namespace ELearningPlatform.Services;

public class LearnerService(ApplicationDbContext context) : ILearnerService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<IEnumerable<LearnerResponse>>> GetAllAsync()
    {
        var learners = await _context.Learners
            .ProjectToType<LearnerResponse>()
            .AsNoTracking()
            .ToListAsync();

        return Result.Success<IEnumerable<LearnerResponse>>(learners);
    }

    public async Task<Result<LearnerResponse>> GetAsync(int id)
    {
        var learner = await _context.Learners.FindAsync(id);
        return learner is not null
            ? Result.Success(learner.Adapt<LearnerResponse>())
            : Result.Failure<LearnerResponse>(LearnerErrors.NotFound);
    }
    public async Task<Result<LearnerResponse>> CreateAsync(CreateLearnerRequest request)
    {
        bool NationalIdExists = await _context.Learners.AnyAsync(l => l.NationalId == request.NationalId);
        if (NationalIdExists)
            return Result.Failure<LearnerResponse>(LearnerErrors.DuplicatedNationalId);

        bool EmailExists = await _context.Learners.AnyAsync(l => l.Email == request.Email);
        if (EmailExists)
            return Result.Failure<LearnerResponse>(LearnerErrors.DuplicatedEmail);

        var learner = request.Adapt<Learner>();

        await _context.Learners.AddAsync(learner);
        await _context.SaveChangesAsync();

        return Result.Success(learner.Adapt<LearnerResponse>());
    }
}

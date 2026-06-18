using ELearningPlatform.Contracts.Enrollment;
using ELearningPlatform.Enums;

namespace ELearningPlatform.Services;

public class EnrollmentService(ApplicationDbContext context) : IEnrollmentService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result> AddAsync(EnrollmentRequest request)
    {
        var learnerExists = await _context.Learners.AnyAsync(x => x.Id == request.LearnerId);
        if (!learnerExists)
            return Result.Failure(LearnerErrors.NotFound);

        var course = await _context.Courses.FindAsync(request.CourseId);
        if (course is null)
            return Result.Failure(CourseErrors.NotFound);

        var enrollmentExists = await _context.Enrollments.AnyAsync(x => x.LearnerId == request.LearnerId && x.CourseId == request.CourseId);
        if (enrollmentExists)
            return Result.Failure(EnrollmentErrors.DuplicatedEnrollment);

        var courseIsActive = await _context.Courses.Where(x => x.Id == request.CourseId).Select(x => x.IsActive).FirstOrDefaultAsync();
        if (!courseIsActive)
            return Result.Failure(CourseErrors.Inactive);

        var enrollment = request.Adapt<Enrollment>();
        enrollment.Status = course.RequiresApproval
            ? EnrollmentStatuses.PendingApproval
            : EnrollmentStatuses.Approved;

        await _context.Enrollments.AddAsync(enrollment);
        await _context.SaveChangesAsync();

        //var response = new EnrollmentResponse(enrollment.LearnerId , enrollment.CourseId , enrollment.EnrolledAt,
        //    enrollment.Status == EnrollmentStatuses.Approved ? "Approved" : "Pending Approval");



        return Result.Success();
    }

    public async Task<Result> MakeDecisionAsync(int id, DecisionRequest request)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment is null)
            return Result.Failure(EnrollmentErrors.NotFound);
        if (enrollment.Status != EnrollmentStatuses.PendingApproval)
            return Result.Failure(EnrollmentErrors.InvalidStatus);
        enrollment.Status = request.Decision == "Approve" ? EnrollmentStatuses.Approved : EnrollmentStatuses.Rejected;

        await _context.SaveChangesAsync();
        return Result.Success();

    }

    public async Task<Result<List<EnrollmentResponse>>> GetEnrollmentsAsync(EnrollmentFilterRequest filters)
    {
        // 1. بناء الاستعلام الأساسي مع جلب الجداول المرتبطة
        var query = _context.Enrollments
            .Include(e => e.Learner)
            .Include(e => e.Course)
            .AsQueryable();


        if (filters.LearnerId.HasValue)
            query = query.Where(e => e.LearnerId == filters.LearnerId.Value); // [cite: 85]

        if (filters.CourseId.HasValue)
            query = query.Where(e => e.CourseId == filters.CourseId.Value); // [cite: 86]

        if (!string.IsNullOrEmpty(filters.Status))
        {
            EnrollmentStatuses? status = filters.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase)
                ? EnrollmentStatuses.Approved
                : filters.Status.Equals("Pending Approval", StringComparison.OrdinalIgnoreCase)
                    ? EnrollmentStatuses.PendingApproval
                    : filters.Status.Equals("Rejected", StringComparison.OrdinalIgnoreCase)
                        ? EnrollmentStatuses.Rejected
                        : (EnrollmentStatuses?)null;

            if (status.HasValue)
            {
                query = query.Where(e => e.Status == status.Value); // [cite: 88]
            }
        }

        if (filters.FromDate.HasValue)
            query = query.Where(e => e.EnrolledAt >= filters.FromDate.Value); // [cite: 89]

        if (filters.ToDate.HasValue)
            query = query.Where(e => e.EnrolledAt <= filters.ToDate.Value); // [cite: 89]

        // 3. التنفيذ
        var enrollments = await query.ToListAsync();

        var response = enrollments.Adapt<List<EnrollmentResponse>>();

        return Result.Success(response);

    }

    
}

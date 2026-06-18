using ELearningPlatform.Enums;

namespace ELearningPlatform.Entities;

public class Enrollment
{
    public int Id { get; set; }
    public int LearnerId { get; set; }
    public int CourseId { get; set; }
    public EnrollmentStatuses Status { get; set; }
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

    public Learner Learner { get; set; } = default!;
    public Course Course { get; set; } = default!;
}

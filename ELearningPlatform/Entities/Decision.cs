namespace ELearningPlatform.Entities;

public class Decision
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int EnrollmentId { get; set; }
    public Enrollment Enrollment { get; set; } = default!;

}

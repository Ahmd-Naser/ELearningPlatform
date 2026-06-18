namespace ELearningPlatform.Contracts.Enrollment;

public class EnrollmentRequestValidator : AbstractValidator<EnrollmentRequest>
{
    public EnrollmentRequestValidator()
    {
        RuleFor(x => x.CourseId)
            .GreaterThan(0);

        RuleFor(x => x.LearnerId)
            .GreaterThan(0);
    }
}

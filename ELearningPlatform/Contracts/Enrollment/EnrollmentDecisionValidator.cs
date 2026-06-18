namespace ELearningPlatform.Contracts.Enrollment;

public class DecisionRequestValidator : AbstractValidator<DecisionRequest>
{
    public DecisionRequestValidator()
    {
        RuleFor(x => x.Decision)
            .Must(d => d == "Approved" || d == "Rejected")
            .WithMessage("Decision must be either 'Approved' or 'Rejected'.");

        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage("A reason must be provided when the enrollment is rejected.")
            .When(x => x.Decision == "Rejected"); 
    }
}

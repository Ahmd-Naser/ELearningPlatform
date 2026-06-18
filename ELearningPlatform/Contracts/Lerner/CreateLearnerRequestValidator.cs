
namespace ELearningPlatform.Contracts.Lerner;

public class CreateLearnerRequestValidator : AbstractValidator<CreateLearnerRequest>
{
    public CreateLearnerRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.NationalId)
            .NotEmpty()
            .Length(14);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

    }
}

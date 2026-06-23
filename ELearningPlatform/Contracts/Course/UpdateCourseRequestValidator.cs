namespace ELearningPlatform.Contracts.Course;

public class UpdateCourseRequestValidator : AbstractValidator<UpdateCourseRequest>
{
    public UpdateCourseRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(1000);



        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.DurationHours)
            .GreaterThan(0)
            .LessThanOrEqualTo(1000);

    }

}
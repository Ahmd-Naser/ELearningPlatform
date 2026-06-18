namespace ELearningPlatform.Errors;

public static class EnrollmentErrors
{
    public static readonly Error DuplicatedEnrollment =
        new("LearnerErrors.DuplicatedEnrollment", "Already enrolled this Course before", StatusCodes.Status409Conflict);

    public static readonly Error NotFound =
        new("EnrollmentErrors.NotFound", "Enrollment is not found", StatusCodes.Status404NotFound);

    public static readonly Error InvalidStatus =
            new("CourseErrors.InvalidStatus", "Invalid Status", StatusCodes.Status400BadRequest);
}

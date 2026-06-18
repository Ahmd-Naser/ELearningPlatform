namespace ELearningPlatform.Errors;

public static class LearnerErrors
{
    public static readonly Error DuplicatedNationalId =
        new("LearnerErrors.DuplicatedNationalId", "Another Learner with the same National ID is already exist", StatusCodes.Status409Conflict);
 
    public static readonly Error DuplicatedEmail =
        new("LearnerErrors.DuplicatedEmail", "Another Learner with the same Email is already exist", StatusCodes.Status409Conflict);

    public static readonly Error NotFound =
        new("LearnerErrors.NotFound", "Learner is not found", StatusCodes.Status404NotFound);


}

using Application.ClientErrors.ErrorCodes;
using ErrorOr;

namespace Application.ClientErrors.Errors;

public static partial class Errors
{
    public class ResolvedExerciseErrors
    {
        public static Error ExerciseAlreadyResolved = Error.Failure(ResolvedExerciseErrorCodes.ExerciseAlreadyResolved, "Exercise already resolved");
    }
}
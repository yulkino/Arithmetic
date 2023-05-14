using Application.ClientErrors.ErrorCodes;
using ErrorOr;

namespace Application.ClientErrors.Errors;

public static partial class Errors
{
    public static class ExerciseErrors
    {
        public static Error NotFound = Error.NotFound(ExerciseErrorCodes.NotFound, "Exercise does not exist for this game");
        public static Error BeyondAmountSettings = Error.Failure(ExerciseErrorCodes.BeyondAmountSettings, "Exercise beyond the amount settings for this game");
    }
}
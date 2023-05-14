using Application.ClientErrors.ErrorCodes;
using ErrorOr;

namespace Application.ClientErrors.Errors;

public static partial class Errors
{
    public static class SettingsErrors
    {
        public static Error NotFound = Error.NotFound(SettingsErrorCodes.NotFound, "Settings for the user do not exist");
        public static Error Conflict = Error.Conflict(SettingsErrorCodes.Conflict, "Settings cannot be edited after starting game");

        public static class OperationsErrors
        {
            public static Error NotFound = Error.NotFound(SettingsErrorCodes.OperationsErrorCodes.NotFound, "One or more operations do not exist");
        }

        public static class DifficultyErrors
        {
            public static Error NotFound = Error.NotFound(SettingsErrorCodes.DifficultyErrorCodes.NotFound, "Difficulty does not exist");
        }
    }
}
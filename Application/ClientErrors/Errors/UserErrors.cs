using Application.ClientErrors.ErrorCodes;
using ErrorOr;

namespace Application.ClientErrors.Errors;

public static partial class Errors
{
    public class UserErrors
    {
        public static Error NotFound = Error.NotFound(UserErrorCodes.NotFound, "User does not exists");
        public static Error Failure = Error.Failure(UserErrorCodes.Failure, "Failed login attempt");
        public static Error Conflict(string login)
        {
            return Error.Conflict(UserErrorCodes.Conflict, $"User with Login {login} already exists");
        }
    }
}
using Application.ClientErrors.ErrorCodes;
using ErrorOr;

namespace Application.ClientErrors.Errors;

public static partial class Errors
{
    public static class GameErrors
    {
        public static Error NotFound = Error.NotFound(GameErrorCodes.NotFound, "Game does not exist");
    }
}
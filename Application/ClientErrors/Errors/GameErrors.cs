using Application.ClientErrors.ErrorCodes;
using ErrorOr;

namespace Application.ClientErrors.Errors;

public static partial class Errors
{
    public static class GameErrors
    {
        public static Error NotFound = Error.NotFound(GameErrorCodes.NotFound, "Game does not exist");
        public static Error NotOver = Error.Failure(GameErrorCodes.NotOver, "Game not over to view results");
    }
}
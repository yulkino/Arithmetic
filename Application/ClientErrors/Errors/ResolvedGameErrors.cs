using Application.ClientErrors.ErrorCodes;
using ErrorOr;

namespace Application.ClientErrors.Errors;

public static partial class Errors
{
    public static class ResolvedGameErrors
    {
        public static Error NotFound = Error.NotFound(ResolvedGameErrorCodes.NotFound, "Resolved game does not exist for this game");
    }
}
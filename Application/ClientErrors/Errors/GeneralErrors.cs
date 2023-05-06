using Application.ClientErrors.ErrorCodes;
using ErrorOr;
using FluentValidation.Results;

namespace Application.ClientErrors.Errors;

public static partial class Errors
{
    public static class GeneralErrors
    {
        public static Error Validation(ValidationFailure v)
        {
            return Error.Validation(GeneralErrorCodes.Validation,
            $"Field: {v.PropertyName} caused Error Code: {v.ErrorCode} with Error Message: {v.ErrorMessage}");
        }
    }
}
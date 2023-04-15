using AutoMapper;
using ErrorOr;
using FluentValidation.Results;

namespace Application.Mapping;

public class ValidationFailureMappingProfile : Profile
{
    public ValidationFailureMappingProfile() => CreateMap<ValidationFailure, Error>()
            .ConvertUsing(v => Error.Validation("General.Validation",
                    $"Property: {v.PropertyName} caused Error Code: {v.ErrorCode} with Error Message: {v.ErrorMessage}."));
}
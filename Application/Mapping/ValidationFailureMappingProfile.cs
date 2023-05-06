using Application.ClientErrors.Errors;
using AutoMapper;
using ErrorOr;
using FluentValidation.Results;

namespace Application.Mapping;

public class ValidationFailureMappingProfile : Profile
{
    public ValidationFailureMappingProfile() => CreateMap<ValidationFailure, Error>()
        .ConvertUsing(v => Errors.GeneralErrors.Validation(v));
}
using AutoMapper;
using ErrorOr;
using FluentValidation.Results;

namespace Application.Mapping;

public class ValidationFailureMappingProfile : Profile
{
    public ValidationFailureMappingProfile()
    {
        CreateMap<List<ValidationFailure>, List<Error>>()
            .ForMember(list => list,
                o => o.MapFrom(validationFailureList => validationFailureList.Select(v => Error.Validation("General.Validation",
                    $"Property: {v.PropertyName} caused Error Code: {v.ErrorCode} with Error Message: {v.ErrorMessage}.")).ToList()));
    }
}
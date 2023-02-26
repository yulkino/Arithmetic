using Application.Validators.GameValidators;
using Domain.Entity.ExerciseEntities;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Mediators.GameMediator.GetExercise;

public class GetExerciseHandler : IRequestHandler<GetExerciseQuery, ErrorOr<Exercise>>
{
    private readonly IValidator<GetExerciseQuery> _validator;

    public GetExerciseHandler(IValidator<GetExerciseQuery> validator)
    {
        _validator = validator;
    }

    public Task<ErrorOr<Exercise>> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
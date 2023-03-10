using Application.ServiceContracts.Repositories.Read.GameReadRepositories;
using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write.GameWriteRepositories;
using Application.ServiceContracts.Repositories.Write.ResolvedGameWriteRepositories;
using Domain.Entity.ExerciseEntities;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Mediators.GameMediator.SaveExercise;

public class SaveExerciseHandler : IRequestHandler<SaveExerciseCommand, ErrorOr<ResolvedExercise>>
{
    private readonly IExerciseReadRepository _exerciseReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IGameReadRepository _gameReadRepository;
    private readonly IResolvedExerciseWriteRepository _resolvedExerciseWriteRepository;

    public SaveExerciseHandler(IExerciseReadRepository exerciseReadRepository, IExerciseWriteRepository exerciseWriteRepository,
        IUserReadRepository userReadRepository, IGameReadRepository gameReadRepository, 
        IResolvedExerciseWriteRepository resolvedExerciseWriteRepository)
    {
        _exerciseReadRepository = exerciseReadRepository;
        _userReadRepository = userReadRepository;
        _gameReadRepository = gameReadRepository;
        _resolvedExerciseWriteRepository = resolvedExerciseWriteRepository;
    }

    public async Task<ErrorOr<ResolvedExercise>> Handle(SaveExerciseCommand request, CancellationToken cancellationToken)
    {
        var (userId, gameId, exerciseId, answer) = request;

        if (await _userReadRepository.GetUserByIdAsync(userId, cancellationToken) is null)
            return Error.NotFound("General.NotFound", "User does not exist.");

        var game = await _gameReadRepository.GetGameByIdAsync(gameId, userId, cancellationToken);
        if (game is null)
            return Error.NotFound("General.NotFound", "Game does not exist.");

        var exercise = await _exerciseReadRepository.GetExerciseByIdAsync(exerciseId, cancellationToken);
        if(exercise is null)
            return Error.NotFound("General.NotFound", "Exercise does not exist.");

        var resolvedExercise = exercise.Resolve(answer);
        return await _resolvedExerciseWriteRepository.SaveResolvedExerciseAsync(resolvedExercise, cancellationToken);
    }
}
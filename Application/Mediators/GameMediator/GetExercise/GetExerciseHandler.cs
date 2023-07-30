using Application.ClientErrors.Errors;
using Application.ServiceContracts;
using Application.ServiceContracts.Repositories.Read;
using Application.Services;
using Domain.Entity.ExerciseEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.GameMediator.GetExercise;

public class GetExerciseHandler : IRequestHandler<GetExerciseQuery, ErrorOr<Exercise>>
{
    private readonly IGameReadRepository _gameReadRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITimeProvider _timeProvider;
    private readonly IUserReadRepository _userReadRepository;

    public GetExerciseHandler(IGameReadRepository gameReadRepository, IUserReadRepository userReadRepository,
        IUnitOfWork unitOfWork, ITimeProvider timeProvider)
    {
        _gameReadRepository = gameReadRepository;
        _userReadRepository = userReadRepository;
        _unitOfWork = unitOfWork;
        _timeProvider = timeProvider;
    }

    public async Task<ErrorOr<Exercise>> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
    {
        var (userId, gameId) = request;

        var user = await _userReadRepository.GetUserByIdAsync(userId, cancellationToken);
        if (user is null)
            return Errors.UserErrors.NotFound;

        var game = await _gameReadRepository.GetGameByIdAsync(gameId, cancellationToken);
        if (game is null)
            return Errors.GameErrors.NotFound;

        if (game.Exercises.Count == game.Settings.ExerciseCount)
            return Errors.ExerciseErrors.BeyondAmountSettings;

        var nextExercise = game.GiveNextExercise(_timeProvider.Now);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return nextExercise;
    }
}
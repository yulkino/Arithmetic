using Application.ClientErrors.Errors;
using Application.ServiceContracts;
using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity.ExerciseEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.GameMediator.GetExercise;

public class GetExerciseHandler : IRequestHandler<GetExerciseQuery, ErrorOr<Exercise>>
{
    private readonly IGameReadRepository _gameReadRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IGameWriteRepository _gameWriteRepository;

    public GetExerciseHandler(IGameReadRepository gameReadRepository, IUserReadRepository userReadRepository,
        IGameWriteRepository gameWriteRepository ,IUnitOfWork unitOfWork)
    {
        _gameReadRepository = gameReadRepository;
        _userReadRepository = userReadRepository;
        _gameWriteRepository = gameWriteRepository;
        _unitOfWork = unitOfWork;
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

        var nextExercise = game.GiveNextExercise();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return nextExercise;
    }
}
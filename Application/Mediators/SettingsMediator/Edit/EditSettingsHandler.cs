using Application.ClientErrors.Errors;
using Application.ServiceContracts;
using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.SettingsEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.SettingsMediator.Edit;

public class EditSettingsHandler : IRequestHandler<EditSettingsCommand, ErrorOr<Settings>>
{
    private readonly IDifficultiesReadRepository _difficultiesReadRepository;
    private readonly IGameReadRepository _gameReadRepository;
    private readonly IOperationsReadRepository _operationsReadRepository;
    private readonly ISettingsReadRepository _settingsReadRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadRepository _userReadRepository;

    public EditSettingsHandler(ISettingsReadRepository settingsReadRepository,
        IUserReadRepository userReadRepository, IOperationsReadRepository operationsReadRepository,
        IDifficultiesReadRepository difficultiesReadRepository, IGameReadRepository gameReadRepository, 
        IUnitOfWork unitOfWork)
    {
        _settingsReadRepository = settingsReadRepository;
        _userReadRepository = userReadRepository;
        _operationsReadRepository = operationsReadRepository;
        _difficultiesReadRepository = difficultiesReadRepository;
        _gameReadRepository = gameReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Settings>> Handle(EditSettingsCommand request, CancellationToken cancellationToken)
    {
        var (userId, gameId, operations, difficulty, exerciseCount) = request;

        var user = await _userReadRepository.GetUserByIdAsync(userId, cancellationToken);
        if (user is null)
            return Errors.UserErrors.NotFound;

        var game = await _gameReadRepository.GetGameByIdAsync(gameId, cancellationToken);
        if(game is null)
            return Errors.GameErrors.NotFound;

        if(game.Exercises.Any())
            return Errors.SettingsErrors.Conflict;

        var settings = await _settingsReadRepository.GetSettingsAsync(game, cancellationToken);
        if (settings is null)
            return Errors.SettingsErrors.NotFound;

        var settingsOperations = await _operationsReadRepository.GetOperationsByIdsAsync(operations, cancellationToken);
        if (settingsOperations.Count is 0)
            return Errors.SettingsErrors.OperationsErrors.NotFound;

        var settingsDifficulty = await _difficultiesReadRepository.GetDifficultyByIdAsync(difficulty, cancellationToken);
        if (settingsDifficulty is null)
            return Errors.SettingsErrors.DifficultyErrors.NotFound;

        settings.Operations = settingsOperations;
        settings.Difficulty = settingsDifficulty;
        settings.ExerciseCount = exerciseCount;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return settings;
    }
}
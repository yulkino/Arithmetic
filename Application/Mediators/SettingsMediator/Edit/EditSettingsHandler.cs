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
    private readonly IOperationsReadRepository _operationsReadRepository;
    private readonly ISettingsReadRepository _settingsReadRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadRepository _userReadRepository;

    public EditSettingsHandler(ISettingsReadRepository settingsReadRepository,
        IUserReadRepository userReadRepository, IOperationsReadRepository operationsReadRepository,
        IDifficultiesReadRepository difficultiesReadRepository, IUnitOfWork unitOfWork)
    {
        _settingsReadRepository = settingsReadRepository;
        _userReadRepository = userReadRepository;
        _operationsReadRepository = operationsReadRepository;
        _difficultiesReadRepository = difficultiesReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Settings>> Handle(EditSettingsCommand request, CancellationToken cancellationToken)
    {
        var (userId, operations, difficulty, exerciseCount) = request;

        if (await _userReadRepository.GetUserByIdAsync(userId, cancellationToken) is null)
            return Errors.UserErrors.NotFound;

        var settings = await _settingsReadRepository.GetSettingsAsync(userId, cancellationToken);
        if (settings is null)
            return Errors.SettingsErrors.NotFound;

        var settingsOperations = await _operationsReadRepository.GetOperationsByIdsAsync(operations, cancellationToken);
        if (settingsOperations.Count is 0)
            return Errors.SettingsErrors.Operations.NotFound;

        var settingsDifficulty =
            await _difficultiesReadRepository.GetDifficultyByIdAsync(difficulty, cancellationToken);
        if (settingsDifficulty is null)
            return Errors.SettingsErrors.Difficulty.NotFound;

        settings.Operations = settingsOperations;
        settings.Difficulty = settingsDifficulty;
        settings.ExerciseCount = exerciseCount;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return settings;
    }
}
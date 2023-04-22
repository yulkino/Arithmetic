using Application.ServiceContracts;
using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Application.ServiceContracts.Repositories.Write;
using AutoMapper;
using Domain.Entity.SettingsEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.SettingsMediator.Edit;

public class EditSettingsHandler : IRequestHandler<EditSettingsCommand, ErrorOr<Settings>>
{
    private readonly IDifficultiesReadRepository _difficultiesReadRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOperationsReadRepository _operationsReadRepository;
    private readonly ISettingsReadRepository _settingsReadRepository;
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
        {
            return Error.NotFound("User.NotFound", "User does not exists.");
        }

        var settings = await _settingsReadRepository.GetSettingsAsync(userId, cancellationToken);
        if (settings is null)
        {
            return Error.NotFound("Settings.NotFound", "User settings do not exist.");
        }

        var settingsOperations = await _operationsReadRepository.GetOperationsByIdsAsync(operations, cancellationToken);
        if (settingsOperations.Count is 0)
        {
            return Error.NotFound("Operations.NotFound", "One or more operations do not exist.");
        }

        var settingsDifficulty = await _difficultiesReadRepository.GetDifficultyByIdAsync(difficulty, cancellationToken);
        if (settingsDifficulty is null)
        {
            return Error.NotFound("Difficulty.NotFound", "Difficulty does not exist.");
        }

        settings.Operations = settingsOperations;
        settings.Difficulty = settingsDifficulty;
        settings.ExerciseCount = exerciseCount;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return settings;
    }
}
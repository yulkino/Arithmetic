using Application.ClientErrors.Errors;
using Application.ServiceContracts;
using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Application.Services.SettingsProvider;
using Domain.Entity.GameEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.GameMediator.Add;

public class AddGameHandler : IRequestHandler<AddGameCommand, ErrorOr<Game>>
{
    private readonly IDefaultSettingsProvider _defaultSettingsProvider;
    private readonly IGameWriteRepository _gameWriteRepository;
    private readonly IResolvedGameWriteRepository _resolvedGameWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadRepository _userReadRepository;

    public AddGameHandler(IGameWriteRepository gameWriteRepository, IUserReadRepository userReadRepository,
        IResolvedGameWriteRepository resolvedGameWriteRepository, IDefaultSettingsProvider defaultSettingsProvider,
        IUnitOfWork unitOfWork)
    {
        _gameWriteRepository = gameWriteRepository;
        _userReadRepository = userReadRepository;
        _resolvedGameWriteRepository = resolvedGameWriteRepository;
        _defaultSettingsProvider = defaultSettingsProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Game>> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        if (await _userReadRepository.GetUserByIdAsync(userId, cancellationToken) is null)
            return Errors.UserErrors.NotFound;

        var game = await _gameWriteRepository.CreateAsync(userId, _defaultSettingsProvider.GetDefaultSettings(),
            cancellationToken);
        await _resolvedGameWriteRepository.CreateResolvedGameAsync(game, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return game;
    }
}
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
    private readonly ITimeProvider _timeProvider;
    private readonly IUserReadRepository _userReadRepository;

    public AddGameHandler(IGameWriteRepository gameWriteRepository, IUserReadRepository userReadRepository,
        IResolvedGameWriteRepository resolvedGameWriteRepository, IDefaultSettingsProvider defaultSettingsProvider,
        IUnitOfWork unitOfWork, ITimeProvider timeProvider)
    {
        _gameWriteRepository = gameWriteRepository;
        _userReadRepository = userReadRepository;
        _resolvedGameWriteRepository = resolvedGameWriteRepository;
        _defaultSettingsProvider = defaultSettingsProvider;
        _unitOfWork = unitOfWork;
        _timeProvider = timeProvider;
    }

    public async Task<ErrorOr<Game>> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        var user = await _userReadRepository.GetUserByIdAsync(userId, cancellationToken);
        if (user is null)
            return Errors.UserErrors.NotFound;

        var settings = await _defaultSettingsProvider.GetDefaultSettingsAsync(cancellationToken);
        var game = await _gameWriteRepository.CreateAsync(user, 
            settings,
            _timeProvider.Now,
            cancellationToken);
        await _resolvedGameWriteRepository.CreateResolvedGameAsync(game, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return game;
    }
}
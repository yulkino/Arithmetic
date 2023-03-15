using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write.GameWriteRepositories;
using Application.ServiceContracts.Repositories.Write.ResolvedGameWriteRepositories;
using Domain.Entity.GameEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.GameMediator.Add;

public class AddGameHandler : IRequestHandler<AddGameCommand, ErrorOr<Game>>
{
    private readonly IGameWriteRepository _gameWriteRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IResolvedGameWriteRepository _resolvedGameWriteRepository;

    public AddGameHandler(IGameWriteRepository gameWriteRepository, IUserReadRepository userReadRepository,
        IResolvedGameWriteRepository resolvedGameWriteRepository)
    {
        _gameWriteRepository = gameWriteRepository;
        _userReadRepository = userReadRepository;
        _resolvedGameWriteRepository = resolvedGameWriteRepository;
    }

    public async Task<ErrorOr<Game>> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        if (await _userReadRepository.GetUserByIdAsync(userId, cancellationToken) is null)
            return Error.NotFound("User.NotFound", "User does not exist.");

        var game = await _gameWriteRepository.CreateAsync(userId, cancellationToken);
        await _resolvedGameWriteRepository.CreateResolvedGameAsync(game, cancellationToken);
        return game;
    }
}
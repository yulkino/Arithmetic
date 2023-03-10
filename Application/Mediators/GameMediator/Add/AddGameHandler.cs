using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write.GameWriteRepositories;
using Domain.Entity.GameEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.GameMediator.Add;

public class AddGameHandler : IRequestHandler<AddGameCommand, ErrorOr<Game>>
{
    private readonly IGameWriteRepository _gameWriteRepository;
    private readonly IUserReadRepository _userReadRepository;

    public AddGameHandler(IGameWriteRepository gameWriteRepository, IUserReadRepository userReadRepository)
    {
        _gameWriteRepository = gameWriteRepository;
        _userReadRepository = userReadRepository;
    }

    public async Task<ErrorOr<Game>> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        if (await _userReadRepository.GetUserByIdAsync(userId, cancellationToken) is null)
            return Error.NotFound("General.NotFound", "User does not exist.");
        return await _gameWriteRepository.CreateAsync(userId, cancellationToken);
    }
}
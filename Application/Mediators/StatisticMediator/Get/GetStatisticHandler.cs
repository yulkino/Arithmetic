using Application.ServiceContracts.Repositories.Read;
using Domain.Entity;
using Domain.Entity.GameEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.StatisticMediator.Get;

public class GetStatisticHandler : IRequestHandler<GetStatisticQuery, ErrorOr<Statistic>>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IStatisticReadRepository _statisticReadRepository;
    private readonly IResolvedGameReadRepository _resolvedGameReadRepository;

    public GetStatisticHandler(IUserReadRepository userReadRepository, IStatisticReadRepository statisticReadRepository,
        IResolvedGameReadRepository resolvedGameReadRepository)
    {
        _userReadRepository = userReadRepository;
        _statisticReadRepository = statisticReadRepository;
        _resolvedGameReadRepository = resolvedGameReadRepository;
    }

    public async Task<ErrorOr<Statistic>> Handle(GetStatisticQuery request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        var user = await _userReadRepository.GetUserByIdAsync(userId, cancellationToken);
        if (user is null)
            return Error.NotFound("General.NotFound", "User does not exist.");

        var userResolvedGames = await _resolvedGameReadRepository.GetUsersGamesAsync(userId, cancellationToken);
        if (userResolvedGames.Count == 0)
            return Error.Custom(204, "General.Empty", "User has not any games.");

        var userStatistic = await _statisticReadRepository.GetUserStatisticAsync(userId, cancellationToken);
        if (userStatistic is null) 
            return GetStatistic(user, userResolvedGames);

        return userStatistic.ResolvedGame.OrderBy(g => g.Id).SequenceEqual(userResolvedGames.OrderBy(g => g.Id)) ? 
            userStatistic : GetStatistic(user, userResolvedGames);
    }

    private Statistic GetStatistic(User user, List<ResolvedGame> resolvedGames)
    {
        var statistic = new Statistic(user, resolvedGames);
        //TODO implement logic
        return statistic;
    }
}
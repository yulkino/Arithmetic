using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Application.Services.StatisticServices;
using Domain.Entity;
using ErrorOr;
using MediatR;

namespace Application.Mediators.StatisticMediator.Get;

public class GetStatisticHandler : IRequestHandler<GetStatisticQuery, ErrorOr<Statistic>>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IStatisticReadRepository _statisticReadRepository;
    private readonly IResolvedGameReadRepository _resolvedGameReadRepository;
    private readonly IStatisticWriteRepository _statisticWriteRepository;
    private readonly IStatisticCollector _statisticCollector;

    public GetStatisticHandler(IUserReadRepository userReadRepository, IStatisticReadRepository statisticReadRepository,
        IResolvedGameReadRepository resolvedGameReadRepository, IStatisticWriteRepository statisticWriteRepository,
        IStatisticCollector statisticCollector)
    {
        _userReadRepository = userReadRepository;
        _statisticReadRepository = statisticReadRepository;
        _resolvedGameReadRepository = resolvedGameReadRepository;
        _statisticWriteRepository = statisticWriteRepository;
        _statisticCollector = statisticCollector;
    }

    public async Task<ErrorOr<Statistic>> Handle(GetStatisticQuery request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        var user = await _userReadRepository.GetUserByIdAsync(userId, cancellationToken);
        if (user is null)
            return Error.NotFound("User.NotFound", "User does not exist.");

        var userResolvedGames = await _resolvedGameReadRepository.GetUsersGamesAsync(userId, cancellationToken);
        if (userResolvedGames.Count == 0)
            return Error.Custom(204, "ResolvedGame.Empty", "User has not any games.");

        var userStatistic = await _statisticReadRepository.GetUserStatisticAsync(userId, cancellationToken);
        if (userStatistic is null)
            return await _statisticWriteRepository.CreateUserStatistic(_statisticCollector.CollectStatistics(user, userResolvedGames), cancellationToken);

        var doNotNeedToUpdate = userStatistic.ResolvedGame
            .OrderBy(g => g.Id)
            .SequenceEqual(userResolvedGames
                .OrderBy(g => g.Id));
        if (doNotNeedToUpdate)
            return userStatistic;

        //TODO implement really update statistic
        return await _statisticWriteRepository.UpdateUserStatistic(_statisticCollector.CollectStatistics(user, userResolvedGames), userStatistic, cancellationToken);
    }
}
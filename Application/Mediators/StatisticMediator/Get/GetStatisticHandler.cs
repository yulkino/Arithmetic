using Application.ClientErrors.Errors;
using Application.ServiceContracts;
using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Application.Services.StatisticServices;
using Domain.Entity;
using ErrorOr;
using MediatR;

namespace Application.Mediators.StatisticMediator.Get;

public class GetStatisticHandler : IRequestHandler<GetStatisticQuery, ErrorOr<Statistic>>
{
    private readonly IResolvedGameReadRepository _resolvedGameReadRepository;
    private readonly IStatisticCollector _statisticCollector;
    private readonly IStatisticReadRepository _statisticReadRepository;
    private readonly IStatisticWriteRepository _statisticWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadRepository _userReadRepository;

    public GetStatisticHandler(IUserReadRepository userReadRepository, IStatisticReadRepository statisticReadRepository,
        IResolvedGameReadRepository resolvedGameReadRepository, IStatisticWriteRepository statisticWriteRepository,
        IStatisticCollector statisticCollector, IUnitOfWork unitOfWork)
    {
        _userReadRepository = userReadRepository;
        _statisticReadRepository = statisticReadRepository;
        _resolvedGameReadRepository = resolvedGameReadRepository;
        _statisticWriteRepository = statisticWriteRepository;
        _statisticCollector = statisticCollector;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Statistic>> Handle(GetStatisticQuery request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        var user = await _userReadRepository.GetUserByIdAsync(userId, cancellationToken);
        if (user is null)
            return Errors.UserErrors.NotFound;

        var userResolvedGames = await _resolvedGameReadRepository.GetUsersResolvedGamesAsync(user, cancellationToken);
        if (userResolvedGames.Count == 0)
            return new Statistic(user, userResolvedGames);

        var userStatistic = await _statisticReadRepository.GetUserStatisticAsync(user, cancellationToken);
        if (userStatistic is null)
        {
            userStatistic = await _statisticCollector.CollectStatistics(user, userResolvedGames, cancellationToken);
            await _statisticWriteRepository.CreateUserStatistic(userStatistic, cancellationToken);
        }
        else
        {
            await _statisticCollector.UpdateStatistics(user, userResolvedGames, userStatistic, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return userStatistic;
    }
}
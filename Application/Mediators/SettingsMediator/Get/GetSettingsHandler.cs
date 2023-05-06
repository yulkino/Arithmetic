using Application.ClientErrors.Errors;
using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.SettingsEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.SettingsMediator.Get;

public class GetSettingsHandler : IRequestHandler<GetSettingsQuery, ErrorOr<Settings>>
{
    private readonly ISettingsReadRepository _settingsReadRepository;
    private readonly IUserReadRepository _userReadRepository;

    public GetSettingsHandler(ISettingsReadRepository settingsReadRepository, IUserReadRepository userReadRepository)
    {
        _settingsReadRepository = settingsReadRepository;
        _userReadRepository = userReadRepository;
    }

    public async Task<ErrorOr<Settings>> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        if (await _userReadRepository.GetUserByIdAsync(userId, cancellationToken) is null)
            return Errors.UserErrors.NotFound;

        var settings = await _settingsReadRepository.GetSettingsAsync(userId, cancellationToken);
        if (settings is null)
            return Errors.SettingsErrors.NotFound;

        return settings;
    }
}
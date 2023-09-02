using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.SettingsEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.OperationMediator.Get;

public class GetOperationsHandler : IRequestHandler<GetOperationsQuery, ErrorOr<HashSet<Operation>>>
{
    private readonly IOperationsReadRepository _operationsReadRepository;

    public GetOperationsHandler(IOperationsReadRepository operationsReadRepository) =>
        _operationsReadRepository = operationsReadRepository;

    public async Task<ErrorOr<HashSet<Operation>>> Handle(GetOperationsQuery request, CancellationToken cancellationToken)
    {
        return await _operationsReadRepository.GetOperationsAsync(cancellationToken);
    }
}
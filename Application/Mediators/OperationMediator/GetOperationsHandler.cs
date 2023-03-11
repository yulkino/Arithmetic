using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.SettingsEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.OperationMediator;

public class GetOperationsHandler : IRequestHandler<GetOperationsQuery, ErrorOr<List<Operation>>>
{
    private readonly IOperationsReadRepository _operationsReadRepository;

    public GetOperationsHandler(IOperationsReadRepository operationsReadRepository)
    {
        _operationsReadRepository = operationsReadRepository;
    }

    public async Task<ErrorOr<List<Operation>>> Handle(GetOperationsQuery request, CancellationToken cancellationToken) 
        => await _operationsReadRepository.GetOperationsAsync(cancellationToken);
}
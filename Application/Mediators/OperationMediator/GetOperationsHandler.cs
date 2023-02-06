using Domain.Entity.SettingsEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.OperationMediator;

public class GetOperationsHandler : IRequestHandler<GetOperationsQuery, ErrorOr<List<Operation>>>
{
    public Task<ErrorOr<List<Operation>>> Handle(GetOperationsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
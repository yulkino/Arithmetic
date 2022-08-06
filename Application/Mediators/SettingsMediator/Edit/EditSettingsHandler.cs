using Domain.Entity;
using ErrorOr;
using MediatR;

namespace Application.Mediators.SettingsMediator.Edit;

public class EditSettingsHandler : IRequestHandler<EditSettingsCommand, ErrorOr<Settings>>
{
    public Task<ErrorOr<Settings>> Handle(EditSettingsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
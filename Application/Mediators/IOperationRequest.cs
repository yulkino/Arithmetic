using ErrorOr;
using MediatR;

namespace Application.Mediators;

public interface IOperationRequest<T> : IRequest<ErrorOr<T>>
{
}
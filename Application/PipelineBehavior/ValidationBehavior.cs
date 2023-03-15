using Application.Mediators;
using AutoMapper;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.PipelineBehavior;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, ErrorOr<TResponse>>
    where TRequest : class, IOperationRequest<TResponse>
{
    private readonly IValidator<TRequest> _validator;
    private readonly IMapper _mapper;

    public ValidationBehavior(IValidator<TRequest> validator, IMapper mapper)
    {
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<ErrorOr<TResponse>> Handle(TRequest request, RequestHandlerDelegate<ErrorOr<TResponse>> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var validationResult = await _validator.ValidateAsync(context, cancellationToken);

        if (validationResult.IsValid)
            return await next();

        return ErrorOr<TResponse>.From(_mapper.Map<List<Error>>(validationResult.Errors));
    }
}
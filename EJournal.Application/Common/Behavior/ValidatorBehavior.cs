using EJournal.Application.Common.Exception;
using FluentValidation;
using MediatR;

namespace EJournal.Application.Common.Behavior;

public sealed class ValidatorBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var validationContext = new ValidationContext<TRequest>(request);

        var errors = _validators
            .Select(x => x.Validate(validationContext))
            .SelectMany(e => e.Errors)
            .Where(e => e != null)
            .Select(e => e.ErrorMessage)
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            throw new BadRequestValidatorsException(errors);
        }

        return await next();
    }
}
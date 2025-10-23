using FluentValidation;
using MediatR;

namespace ByteTech.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))
            );

            var firstFailure = validationResults
                .SelectMany(r => r.Errors)
                .FirstOrDefault(f => f != null);

            if (firstFailure is not null)
            {
                throw new ValidationException(firstFailure.ErrorMessage);
            }
        }

        return await next();
    }
}
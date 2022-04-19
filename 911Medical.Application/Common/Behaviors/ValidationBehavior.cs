using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _911Medical.Application.Common.Behaviors
{
    /// <summary>
    /// Mediatr validation behavior executed as part of mediatr pipeline before command handler is executed.
    /// </summary>
    /// <typeparam name="TRequest">Type of request</typeparam>
    /// <typeparam name="TResponse">Type of response</typeparam>
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// List of validators associated with TRequest type
        /// </summary>
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Constructs validation behavior for Mediatr
        /// </summary>
        /// <param name="validators">Validators associated with TRequest type</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Request handler for this behavior.
        /// </summary>
        /// <param name="request">Instance of TRequest</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="next">Next request handler in the Mediatr pipeline.</param>
        /// <returns></returns>
        /// <exception cref="FluentValidation.ValidationException"></exception>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // if there's any validators associated with request type, validate them
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                // Execute all validators
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                // If failure was reported by any validator, throw exception to break pipeline execution.
                if (failures.Count != 0)
                {
                    throw new FluentValidation.ValidationException(failures);
                }
            }

            // No validation errors detected,
            // so we can execute the next command/query handler.
            return await next();
        }
    }
}

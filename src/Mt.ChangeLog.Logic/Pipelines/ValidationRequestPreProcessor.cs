using FluentValidation;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Logic.Models;
using Mt.Utilities;
using Mt.Utilities.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Pipelines
{
    /// <summary>
	/// Препроцессор валидации.
	/// </summary>
	/// <typeparam name="TRequest">Тип запроса.</typeparam>
    public sealed class ValidationRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest> where TRequest : IMtRequest, IValidatedRequest
    {
        /// <summary>
		/// Журнал логирования.
		/// </summary>
		private readonly ILogger<ValidationRequestPreProcessor<TRequest>> logger;

        /// <summary>
        /// Валидатор данных запроса.
        /// </summary>
        private readonly IValidator<TRequest> validator;

        /// <summary>
		/// Инициализация экземпляра класса <see cref="ValidationRequestPreProcessor{TRequest}"/>.
		/// </summary>
		/// <param name="logger">Журнал логирования.</param>
		/// <param name="validator">Валидатор данных запроса.</param>
		/// <exception cref="ArgumentNullException">Срабатывает если входные параметры равны null.</exception>
		public ValidationRequestPreProcessor(ILogger<ValidationRequestPreProcessor<TRequest>> logger, IValidator<TRequest> validator)
        {
            this.logger = Check.NotNull(logger, nameof(logger));
            this.validator = Check.NotNull(validator, nameof(validator));
        }

        /// <inheritdoc />
		public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            logger.LogInformation($"Mt request: '{request.Guid}', username: '{request.UserName}' - валидация параметров.");
            try
            {
                await validator.ValidateAsync(
                    request,
                    options =>
                    {
                        options.IncludeRuleSets("default", "command");
                        options.ThrowOnFailures();
                    },
                    cancellationToken);
            }
            catch (ValidationException exception)
            {
                var properties = string.Join(", ", exception.Errors.Select(prop =>
                {
                    return prop.PropertyName.Substring(prop.PropertyName.LastIndexOf('.') + 1);
                }).Distinct());

                throw new MtException(exception, ErrorCode.EntityValidation, $"Ошибка валидации параметров: {properties}.");
            }
        }
    }
}
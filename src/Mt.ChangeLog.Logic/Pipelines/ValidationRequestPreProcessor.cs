using FluentValidation;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Logic.Models;
using Mt.Utilities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Pipelines
{
    /// <summary>
	/// Препроцессор валидации.
	/// </summary>
	/// <typeparam name="TRequest">Тип запроса.</typeparam>
    public sealed class ValidationRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest> where TRequest : IMtRequest
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
		public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            logger.LogInformation($"{request} - валидация параметров.");
            return Task.CompletedTask;
        }
    }
}
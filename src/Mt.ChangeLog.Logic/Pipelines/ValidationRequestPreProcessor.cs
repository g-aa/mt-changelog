using FluentValidation;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Logic.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Pipelines;

/// <summary>
/// Препроцессор валидации.
/// </summary>
/// <typeparam name="TRequest">Тип запроса.</typeparam>
public sealed class ValidationRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger<ValidationRequestPreProcessor<TRequest>> _logger;

    private readonly IMtUser _user;

    private readonly IValidator<TRequest>? _validator;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ValidationRequestPreProcessor{TRequest}"/>.
    /// </summary>
    /// <param name="logger">Журнал логирования.</param>
    /// <param name="user">Пользователь системы МТ.</param>
    /// <param name="validator">Валидатор данных запроса.</param>
    public ValidationRequestPreProcessor(
        ILogger<ValidationRequestPreProcessor<TRequest>> logger,
        IMtUser user,
        IValidator<TRequest>? validator = null)
    {
        _logger = logger;
        _user = user;
        _validator = validator;
    }

    /// <inheritdoc />
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        using var scope = _logger.BeginWithMtUserScope(_user);
        if (_validator == null)
        {
            _logger.LogWarning("Для запроса '{Name}' не предусмотрен набор правил валидации.", request.GetType().FullName);
            return;
        }

        _logger.LogDebug("Начат процесс валидации параметров запроса.");
        try
        {
            await _validator.ValidateAsync(
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
            var properties = string.Join(", ", exception.Errors.Select(p =>
            {
                return p.PropertyName.Substring(p.PropertyName.LastIndexOf('.') + 1);
            }).Distinct());

            throw new MtException(exception, ErrorCode.EntityValidation, $"Ошибка валидации параметров: {properties}.");
        }
    }
}
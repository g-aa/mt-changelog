using System.Diagnostics.CodeAnalysis;

using Mt.ChangeLog.Logic;

namespace Mt.ChangeLog.WebAPI.Infrastructure;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public sealed record MtUser : IMtUser
{
    /// <summary>
    /// Наименование пользователя по умолчанию.
    /// </summary>
    public const string DefaultName = "mt-root-user";

    /// <summary>
    /// Инициализатор экземпляра класса <see cref="MtUser"/>.
    /// </summary>
    /// <param name="httpContextAccessor">Механизм доступа к HTTP контексту.</param>
    public MtUser(IHttpContextAccessor httpContextAccessor)
    {
        var principal = httpContextAccessor.HttpContext?.User;
        Name = principal?.Identity?.Name ?? DefaultName;
    }

    /// <inheritdoc />
    public string Name { get; init; }
}
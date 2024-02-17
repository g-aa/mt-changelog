using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Platform;

namespace Mt.ChangeLog.Logic.Converters;

/// <summary>
/// Преобразователь объектов для platforms.
/// </summary>
public static class PlatformConverters
{
    /// <summary>
    /// Преобразовать сущность <see cref="PlatformEntity"/> в модель <see cref="PlatformShortModel"/>.
    /// </summary>
    public sealed class EntityToShortModelConverter : IConverter<PlatformEntity, PlatformShortModel>
    {
        /// <inheritdoc />
        public PlatformShortModel Convert(PlatformEntity source)
        {
            return new PlatformShortModel
            {
                Id = source.Id,
                Title = source.Title,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="PlatformEntity"/> в модель <see cref="PlatformTableModel"/>.
    /// </summary>
    public sealed class EntityToTableModelConverter : IConverter<PlatformEntity, PlatformTableModel>
    {
        /// <inheritdoc />
        public PlatformTableModel Convert(PlatformEntity source)
        {
            return new PlatformTableModel
            {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="PlatformEntity"/> в модель <see cref="PlatformModel"/>.
    /// </summary>
    public sealed class EntityToModelConverter : IConverter<PlatformEntity, PlatformModel>
    {
        private readonly IConverter<AnalogModuleEntity, AnalogModuleShortModel> _converter;

        /// <summary>
        /// Инициализатор нового экземпляра класса <see cref="EntityToModelConverter"/>.
        /// </summary>
        /// <param name="converter">Конвертер.</param>
        public EntityToModelConverter(IConverter<AnalogModuleEntity, AnalogModuleShortModel> converter)
        {
            _converter = converter;
        }

        /// <inheritdoc />
        public PlatformModel Convert(PlatformEntity source)
        {
            return new PlatformModel
            {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
                AnalogModules = source.AnalogModules.Select(_converter.Convert).ToList(),
            };
        }
    }
}
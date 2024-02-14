using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Platform;

namespace Mt.ChangeLog.Logic.Converters;

/// <summary>
/// Преобразователь объектов для analog modules.
/// </summary>
public static class AnalogModuleConverters
{
    /// <summary>
    /// Преобразовать сущность <see cref="AnalogModuleEntity"/> в модель <see cref="AnalogModuleShortModel"/>.
    /// </summary>
    public sealed class EntityToShortModelConverter : IConverter<AnalogModuleEntity, AnalogModuleShortModel>
    {
        /// <inheritdoc />
        public AnalogModuleShortModel Convert(AnalogModuleEntity source)
        {
            return new AnalogModuleShortModel
            {
                Id = source.Id,
                Title = source.Title,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="AnalogModuleEntity"/> в модель <see cref="AnalogModuleTableModel"/>.
    /// </summary>
    public sealed class EntityToTableModelConverter : IConverter<AnalogModuleEntity, AnalogModuleTableModel>
    {
        /// <inheritdoc />
        public AnalogModuleTableModel Convert(AnalogModuleEntity source)
        {
            return new AnalogModuleTableModel
            {
                Id = source.Id,
                Title = source.Title,
                Current = source.Current,
                DIVG = source.DIVG,
                Description = source.Description,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="AnalogModuleEntity"/> в модель <see cref="AnalogModuleModel"/>.
    /// </summary>
    public sealed class EntityToModelConverter : IConverter<AnalogModuleEntity, AnalogModuleModel>
    {
        private readonly IConverter<PlatformEntity, PlatformShortModel> _converter;

        /// <summary>
        /// Инициализатор нового экземпляра класса <see cref="EntityToModelConverter"/>.
        /// </summary>
        /// <param name="converter">Конвертер.</param>
        public EntityToModelConverter(IConverter<PlatformEntity, PlatformShortModel> converter)
        {
            _converter = converter;
        }

        /// <inheritdoc />
        public AnalogModuleModel Convert(AnalogModuleEntity source)
        {
            return new AnalogModuleModel
            {
                Id = source.Id,
                Title = source.Title,
                DIVG = source.DIVG,
                Current = source.Current,
                Description = source.Description,
                Platforms = source.Platforms.Select(_converter.Convert).ToList(),
            };
        }
    }
}
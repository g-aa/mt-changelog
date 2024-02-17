using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.Protocol;

namespace Mt.ChangeLog.Logic.Converters;

/// <summary>
/// Преобразователь объектов для protocols.
/// </summary>
public static class ProtocolConvertors
{
    /// <summary>
    /// Преобразовать сущность <see cref="ProtocolEntity"/> в модель <see cref="ProtocolShortModel"/>.
    /// </summary>
    public sealed class EntityToShortModelConverter : IConverter<ProtocolEntity, ProtocolShortModel>
    {
        /// <inheritdoc />
        public ProtocolShortModel Convert(ProtocolEntity source)
        {
            return new ProtocolShortModel
            {
                Id = source.Id,
                Title = source.Title,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProtocolEntity"/> в модель <see cref="ProtocolTableModel"/>.
    /// </summary>
    public sealed class EntityToTableModelConverter : IConverter<ProtocolEntity, ProtocolTableModel>
    {
        /// <inheritdoc />
        public ProtocolTableModel Convert(ProtocolEntity source)
        {
            return new ProtocolTableModel
            {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProtocolEntity"/> в модель <see cref="ProtocolModel"/>.
    /// </summary>
    public sealed class EntityToModelConverter : IConverter<ProtocolEntity, ProtocolModel>
    {
        private readonly IConverter<CommunicationEntity, CommunicationShortModel> _converter;

        /// <summary>
        /// Инициализатор нового экземпляра класса <see cref="EntityToModelConverter"/>.
        /// </summary>
        /// <param name="converter">Конвертер.</param>
        public EntityToModelConverter(IConverter<CommunicationEntity, CommunicationShortModel> converter)
        {
            _converter = converter;
        }

        /// <inheritdoc />
        public ProtocolModel Convert(ProtocolEntity source)
        {
            return new ProtocolModel
            {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
                Communications = source.Communications.OrderBy(c => c.Title).Select(_converter.Convert).ToList(),
            };
        }
    }
}
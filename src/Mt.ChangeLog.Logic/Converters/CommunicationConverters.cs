using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.Protocol;

namespace Mt.ChangeLog.Logic.Converters;

/// <summary>
/// Преобразователь объектов для communications.
/// </summary>
public static class CommunicationConverters
{
    /// <summary>
    /// Преобразовать сущность <see cref="CommunicationEntity"/> в модель <see cref="CommunicationShortModel"/>.
    /// </summary>
    public sealed class EntityToShortModelConverter : IConverter<CommunicationEntity, CommunicationShortModel>
    {
        /// <inheritdoc />
        public CommunicationShortModel Convert(CommunicationEntity source)
        {
            return new CommunicationShortModel
            {
                Id = source.Id,
                Title = source.Title,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="CommunicationEntity"/> в модель <see cref="CommunicationTableModel"/>.
    /// </summary>
    public sealed class EntityToTableModelConverter : IConverter<CommunicationEntity, CommunicationTableModel>
    {
        /// <inheritdoc />
        public CommunicationTableModel Convert(CommunicationEntity source)
        {
            return new CommunicationTableModel
            {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
                Protocols = source.Protocols.Count != 0 ? string.Join(", ", source.Protocols.OrderBy(e => e.Title).Select(e => e.Title)) : string.Empty,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="CommunicationEntity"/> в модель <see cref="CommunicationModel"/>.
    /// </summary>
    public sealed class EntityToModelConverter : IConverter<CommunicationEntity, CommunicationModel>
    {
        private readonly IConverter<ProtocolEntity, ProtocolShortModel> _converter;

        /// <summary>
        /// Инициализатор нового экземпляра класса <see cref="EntityToModelConverter"/>.
        /// </summary>
        /// <param name="converter">Конвертер.</param>
        public EntityToModelConverter(IConverter<ProtocolEntity, ProtocolShortModel> converter)
        {
            _converter = converter;
        }

        /// <inheritdoc />
        public CommunicationModel Convert(CommunicationEntity source)
        {
            return new CommunicationModel
            {
                Id = source.Id,
                Title = source.Title,
                Protocols = source.Protocols.OrderBy(p => p.Title).Select(_converter.Convert).ToList(),
                Description = source.Description,
            };
        }
    }
}
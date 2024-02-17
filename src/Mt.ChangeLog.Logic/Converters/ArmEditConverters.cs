using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ArmEdit;

namespace Mt.ChangeLog.Logic.Converters;

/// <summary>
/// Преобразователь объектов для arm edits.
/// </summary>
public static class ArmEditConverters
{
    /// <summary>
    /// Преобразовать сущность <see cref="ArmEditEntity"/> в модель <see cref="ArmEditShortModel"/>.
    /// </summary>
    public sealed class EntityToShortModelConverter : IConverter<ArmEditEntity, ArmEditShortModel>
    {
        /// <inheritdoc />
        public ArmEditShortModel Convert(ArmEditEntity source)
        {
            return new ArmEditShortModel
            {
                Id = source.Id,
                Version = source.Version,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ArmEditEntity"/> в модель <see cref="ArmEditModel"/>.
    /// </summary>
    public sealed class EntityToTableModelConverter : IConverter<ArmEditEntity, ArmEditModel>
    {
        /// <inheritdoc />
        public ArmEditModel Convert(ArmEditEntity source)
        {
            return new ArmEditModel
            {
                Id = source.Id,
                Date = source.Date,
                DIVG = source.DIVG,
                Version = source.Version,
                Description = source.Description,
            };
        }
    }
}
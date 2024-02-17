using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;

namespace Mt.ChangeLog.Logic.Converters;

/// <summary>
/// Преобразователь объектов для relay algorithms.
/// </summary>
public static class RelayAlgorithmConvertors
{
    /// <summary>
    /// Преобразовать сущность <see cref="RelayAlgorithmEntity"/> в модель <see cref="RelayAlgorithmShortModel"/>.
    /// </summary>
    public sealed class EntityToShortModelConverter : IConverter<RelayAlgorithmEntity, RelayAlgorithmShortModel>
    {
        /// <inheritdoc />
        public RelayAlgorithmShortModel Convert(RelayAlgorithmEntity source)
        {
            return new RelayAlgorithmShortModel
            {
                Id = source.Id,
                Title = source.Title,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="RelayAlgorithmEntity"/> в модель <see cref="RelayAlgorithmModel"/>.
    /// </summary>
    public sealed class EntityToModelConverter : IConverter<RelayAlgorithmEntity, RelayAlgorithmModel>
    {
        /// <inheritdoc />
        public RelayAlgorithmModel Convert(RelayAlgorithmEntity source)
        {
            return new RelayAlgorithmModel
            {
                Id = source.Id,
                Group = source.Group,
                Title = source.Title,
                ANSI = source.ANSI,
                LogicalNode = source.LogicalNode,
                Description = source.Description,
            };
        }
    }
}
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Методы расширения для класса <see cref="RelayAlgorithmEntity"/>.
    /// </summary>
    public static class RelayAlgorithmExtensions
    {
        /// <summary>
        /// Преобразовать сущность <see cref="RelayAlgorithmEntity"/> в модель <see cref="RelayAlgorithmShortModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static RelayAlgorithmShortModel ToShortModel(this RelayAlgorithmEntity entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new RelayAlgorithmShortModel()
            {
                Id = entity.Id,
                Title = entity.Title
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="RelayAlgorithmEntity"/> в модель <see cref="RelayAlgorithmModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static RelayAlgorithmModel ToModel(this RelayAlgorithmEntity entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new RelayAlgorithmModel()
            {
                Id = entity.Id,
                Group = entity.Group,
                Title = entity.Title,
                ANSI = entity.ANSI,
                LogicalNode = entity.LogicalNode,
                Description = entity.Description
            };
            return result;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Строитель.</returns>
        public static RelayAlgorithmBuilder GetBuilder(this RelayAlgorithmEntity entity)
        {
            return new RelayAlgorithmBuilder(entity);
        }
    }
}
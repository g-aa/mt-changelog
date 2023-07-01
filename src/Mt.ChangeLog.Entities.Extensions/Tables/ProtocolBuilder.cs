using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Protocol;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Строитель <see cref="ProtocolEntity"/>.
    /// </summary>
    public class ProtocolBuilder
    {
        private readonly ProtocolEntity entity;

        private string title;
        private string description;
        private IQueryable<CommunicationEntity> communications;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="ProtocolBuilder"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <exception cref="ArgumentNullException">Срабатывает если entity равно null.</exception>
        public ProtocolBuilder(ProtocolEntity entity)
        {
            this.entity = Check.NotNull(entity, nameof(entity));
            this.title = entity.Title;
            this.description = entity.Description;
            this.communications = entity.Communications.AsQueryable();
        }

        /// <summary>
        /// Добавить атрибуты.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если model равно null.</exception>
        public ProtocolBuilder SetAttributes(ProtocolModel model)
        {
            Check.NotNull(model, nameof(model));
            this.title = model.Title;
            this.description = model.Description;
            return this;
        }

        /// <summary>
        /// Добавить перечень протоколов.
        /// </summary>
        /// <param name="modules">Перечень протоколов.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если modules равно null.</exception>
        public ProtocolBuilder SetModules(IQueryable<CommunicationEntity> modules)
        {
            this.communications = Check.NotNull(modules, nameof(modules));
            return this;
        }

        /// <summary>
        /// Построить сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        public ProtocolEntity Build()
        {
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.Title = this.title;
            this.entity.Description = this.description;
            // реляционные связи:
            this.entity.Communications = this.communications.ToHashSet();
            return this.entity;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <returns>Строитель.</returns>
        public static ProtocolBuilder GetBuilder()
        {
            return new ProtocolBuilder(new ProtocolEntity());
        }
    }
}
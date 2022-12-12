using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.Utilities;
using System;
using System.Linq;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Строитель <see cref="CommunicationEntity"/>.
    /// </summary>
    public sealed class CommunicationBuilder
    {
        private readonly CommunicationEntity entity;

        private string title;
        private string description;
        private IQueryable<ProtocolEntity> protocols;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="CommunicationBuilder"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <exception cref="ArgumentNullException">Срабатывает если entity равно null.</exception>
        public CommunicationBuilder(CommunicationEntity entity) 
        {
            this.entity = Check.NotNull(entity, nameof(entity));
            this.title = entity.Title;
            this.description = entity.Description;
            this.protocols = entity.Protocols.AsQueryable();
        }

        /// <summary>
        /// Добавить атрибуты.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если model равно null.</exception>
        public CommunicationBuilder SetAttributes(CommunicationModel model)
        {
            Check.NotNull(model, nameof(model));
            this.title = model.Title;
            this.description = model.Description;
            return this;
        }

        /// <summary>
        /// Добавить перечень протоколов.
        /// </summary>
        /// <param name="protocols">Перечень протоколов.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если protocols равно null.</exception>
        public CommunicationBuilder SetProtocols(IQueryable<ProtocolEntity> protocols)
        {
            this.protocols = Check.NotNull(protocols, nameof(protocols));
            return this;
        }

        /// <summary>
        /// Построить сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        public CommunicationEntity Build()
        {
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.Title = this.title;
            this.entity.Description = this.description;
            // реляционные связи:
            this.entity.Protocols = this.protocols.ToHashSet();
            return this.entity;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <returns>Строитель.</returns>
        public static CommunicationBuilder GetBuilder() 
        {
            return new CommunicationBuilder(new CommunicationEntity());
        }
    }
}
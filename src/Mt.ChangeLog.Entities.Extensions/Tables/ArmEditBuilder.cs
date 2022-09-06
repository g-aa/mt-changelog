using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using System;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Строитель <see cref="ArmEdit"/>.
    /// </summary>
    public class ArmEditBuilder
    {
        private readonly ArmEdit entity;

        private string divg;
        private string version;
        private DateTime? date;
        private string description;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="ArmEditBuilder"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        public ArmEditBuilder(ArmEdit entity) 
        {
            this.entity = entity;
        }

        /// <summary>
        /// Добавить атрибуты.
        /// </summary>
        /// <param name="model"Модель.</param>
        /// <returns>Строитель.</returns>
        public ArmEditBuilder SetAttributes(ArmEditModel model)
        {
            this.divg = model?.DIVG;
            this.version = model?.Version;
            this.date = model?.Date;
            this.description = model?.Description;
            return this;
        }

        /// <summary>
        /// Построить сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        /// <exception cref="ArgumentException">Ошибка в логике обработки связей.</exception>
        public ArmEdit Build()
        {
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.DIVG = this.divg;
            this.entity.Version = this.version;
            this.entity.Date = date != null ? date.Value : DateTime.Now;
            this.entity.Description = description;
            // реляционные связи:
            // this.entity.ProjectRevisions - не обновляется!
            return this.entity;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <returns>Строитель.</returns>
        public static ArmEditBuilder GetBuilder()
        {
            return new ArmEditBuilder(new ArmEdit());
        }
    }
}
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;
using Mt.Utilities;
using System;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Строитель <see cref="RelayAlgorithm"/>.
    /// </summary>
    public class RelayAlgorithmBuilder
    {
        private readonly RelayAlgorithm entity;

        private string group;
        private string title;
        private string ansi;
        private string logicalnode;
        private string description;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="RelayAlgorithmBuilder"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <exception cref="ArgumentNullException">Срабатывает если entity равно null.</exception>
        public RelayAlgorithmBuilder(RelayAlgorithm entity) 
        {
            this.entity = Check.NotNull(entity, nameof(entity));
            this.group = entity.Group;
            this.title = entity.Title;
            this.ansi = entity.ANSI;
            this.logicalnode = entity.LogicalNode;
            this.description = entity.Description;
        }

        /// <summary>
        /// Добавить атрибуты.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если model равно null.</exception>
        public RelayAlgorithmBuilder SetAttributes(RelayAlgorithmModel model)
        {
            Check.NotNull(model, nameof(model));
            this.group = model.Group;
            this.title = model.Title;
            this.ansi = model.ANSI;
            this.logicalnode = model.LogicalNode;
            this.description = model.Description;
            return this;
        }

        /// <summary>
        /// Построить сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        public RelayAlgorithm Build()
        {
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.Group = this.group;
            this.entity.Title = this.title;
            this.entity.ANSI = this.ansi;
            this.entity.LogicalNode = this.logicalnode;
            this.entity.Description = this.description;
            // this.entity.ProjectRevisions - не обновляется!
            return this.entity;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <returns>Строитель.</returns>
        public static RelayAlgorithmBuilder GetBuilder() 
        {
            return new RelayAlgorithmBuilder(new RelayAlgorithm());
        }
    }
}
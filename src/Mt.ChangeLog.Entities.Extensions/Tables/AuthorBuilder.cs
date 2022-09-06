using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.Utilities;
using System;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Строитель <see cref="Author"/>.
    /// </summary>
    public sealed class AuthorBuilder
    {
        private readonly Author entity;

        private string firstname;
        private string lastname;
        private string position;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="AuthorBuilder"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        public AuthorBuilder(Author entity) 
        {
            this.entity = Check.NotNull(entity, nameof(entity));
            this.firstname = entity.FirstName;
            this.lastname = entity.LastName;
            this.position = entity.Position;
        }

        /// <summary>
        /// Добавить атрибуты.
        /// </summary>
        /// <param name="model"Модель.</param>
        /// <returns>Строитель.</returns>
        public AuthorBuilder SetAttributes(AuthorModel model)
        {
            Check.NotNull(model, nameof(model));
            this.firstname = model.FirstName;
            this.lastname = model.LastName;
            this.position = model.Position;
            return this;
        }

        /// <summary>
        /// Построить сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        /// <exception cref="ArgumentException">Ошибка в логике обработки связей.</exception>
        public Author Build()
        {
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.FirstName = this.firstname;
            this.entity.LastName = this.lastname;
            this.entity.Position = this.position;
            // реляционные связи:
            // this.entity.ProjectRevisions - не обновляется!
            return this.entity;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <returns>Строитель.</returns>
        public static AuthorBuilder GetBuilder()
        {
            return new AuthorBuilder(new Author());
        }
    }
}
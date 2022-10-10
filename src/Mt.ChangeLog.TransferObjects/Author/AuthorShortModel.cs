using Mt.Utilities;
using System;

namespace Mt.ChangeLog.TransferObjects.Author
{
    /// <summary>
    /// Краткая модель автора.
    /// </summary>
    public class AuthorShortModel
    {
        /// <summary>
        /// ИД.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="AuthorShortModel"/>.
        /// </summary>
        public AuthorShortModel()
        {
            this.Id = Guid.NewGuid();
            this.FirstName = DefaultString.FirstName;
            this.LastName = DefaultString.LastName;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.LastName} {this.FirstName}";
        }
    }
}
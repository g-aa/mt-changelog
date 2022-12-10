using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Author
{
    /// <summary>
    /// Модель автора для таблиц.
    /// </summary>
    public class AuthorTableModel : AuthorShortModel
    {
        /// <summary>
        /// Должность.
        /// </summary>
        /// <example>Инженер-системотехник</example>
        public string Position { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="AuthorTableModel"/>.
        /// </summary>
        public AuthorTableModel() : base()
        {
            this.Position = DefaultString.Position;
        }
    }
}
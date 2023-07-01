using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Communication
{
    /// <summary>
    /// Краткая модель коммуникационного модуля.
    /// </summary>
    public class CommunicationShortModel
    {
        /// <summary>
        /// ИД.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        /// <example>АК Virtual</example>
        public string Title { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="CommunicationShortModel"/>.
        /// </summary>
        public CommunicationShortModel()
        {
            this.Id = Guid.NewGuid();
            this.Title = DefaultString.Communication;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Title;
        }
    }
}
namespace Mt.ChangeLog.TransferObjects.Other
{
    /// <summary>
    /// Модель данных сообщения.
    /// </summary>
    public sealed class MessageModel
    {
        /// <summary>
        /// Сообщение.
        /// </summary>
        /// <example>Текстовое сообщение...</example>
        public string Message { get; set; }

        /// <summary>
        /// Инициализация экземпляра класса <see cref="MessageModel"/>.
        /// </summary>
        public MessageModel()
        {
            this.Message = "Текст сообщения.";
        }
    }
}
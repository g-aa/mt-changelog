namespace Mt.ChangeLog.TransferObjects.Other
{
    /// <summary>
    /// Модель статуса или сообщения для ответов.
    /// </summary>
    public sealed class StatusModel
    {
        /// <summary>
        /// Сообщение.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Инициализация экземпляра класса <see cref="StatusModel"/>.
        /// </summary>
        /// <param name="message"></param>
        public StatusModel(string message = "ok")
        {
            this.Message = message;
        }
    }
}
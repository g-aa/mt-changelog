namespace Mt.ChangeLog.WebAPI.Infrastracture
{
    /// <summary>
    /// Модель описания приложения.
    /// </summary>
    public sealed class MtAppDescriptionModel
    {
        /// <summary>
        /// Версия приложения (Mt-AppName: vX.X.X.X).
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Краткое описание приложения.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Авторские права.
        /// </summary>
        public string Copyright { get; set; }


        /// <summary>
        /// Ссылка на репозиторий.
        /// </summary>
        public string Repository { get; set; }
    }
}
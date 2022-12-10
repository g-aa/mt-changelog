namespace Mt.ChangeLog.WebAPI.Infrastracture
{
    /// <summary>
    /// Модель описания приложения.
    /// </summary>
    public sealed class MtAppDescriptionModel
    {
        /// <summary>
        /// Версия приложения (Mt-ApplicationName: vX.X.X.X).
        /// </summary>
        /// <example>Mt-ApplicationName: v0.0.0.0</example>
        public string Version { get; set; }

        /// <summary>
        /// Краткое описание приложения.
        /// </summary>
        /// <example>Описание приложения...</example>
        public string Description { get; set; }

        /// <summary>
        /// Авторские права.
        /// </summary>
        /// <example>НТЦ Механотроники 1993 – 2022.</example>
        public string Copyright { get; set; }


        /// <summary>
        /// Ссылка на репозиторий.
        /// </summary>
        /// <example>https://github.com/</example>
        public string Repository { get; set; }
    }
}
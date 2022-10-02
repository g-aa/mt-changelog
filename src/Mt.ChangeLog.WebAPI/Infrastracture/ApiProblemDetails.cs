using Mt.Utilities;
using Mt.Utilities.Exceptions;
using Mt.Utilities.Extensions;
using System.Text.Json.Serialization;

namespace Mt.ChangeLog.WebAPI.Infrastracture
{
    /// <summary>
    /// Формат ответа при срабатывании ошибки.
    /// </summary>
    public class ApiProblemDetails
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; private set; }

        /// <summary>
        /// Код ошибки.
        /// </summary>
        [JsonPropertyName("desc")]
        public string Description { get; private set; }

        /// <summary>
        /// Инициализация экземпляра класса <see cref="ApiProblemDetails"/>.
        /// </summary>
        public ApiProblemDetails()
        {
            var code = ErrorCode.InternalLogic;
            this.Title = code.Title();
            this.Description = code.Desc();
        }

        /// <summary>
        /// Инициализация экземпляра класса <see cref="ApiProblemDetails"/>.
        /// </summary>
        /// <param name="code">Код ошибки.</param>
        public ApiProblemDetails(ErrorCode code)
        {
            this.Title = code.Title();
            this.Description = code.Desc();
        }

        /// <summary>
        /// Инициализация экземпляра класса <see cref="ApiProblemDetails"/>.
        /// </summary>
        /// <param name="title">Заголовок.</param>
        /// <param name="description">Описание.</param>
        public ApiProblemDetails(string title, string description)
        {
            this.Title = Check.NotEmpty(title, nameof(title));
            this.Description = Check.NotEmpty(description, nameof(description));
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Title}: {this.Description}";
        }
    }
}
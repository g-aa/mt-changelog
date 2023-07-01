using MediatR;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Models
{
    /// <summary>
    /// Модель запроса используемый в логике MT.
    /// </summary>
    /// <typeparam name="TModel">Тип передаваемой модели данных.</typeparam>
    /// <typeparam name="TResponse">Тип ответа.</typeparam>
    public abstract class MtQuery<TModel, TResponse> : IMtRequest, IRequest<TResponse>
    {
        /// <inheritdoc />
        public Guid Guid { get; private set; }

        /// <inheritdoc />
        public string UserName { get; set; }

        /// <summary>
        /// Модель данных.
        /// </summary>
        public TModel Model { get; private set; }

        /// <summary>
        /// Инициализация экземпляра класса <see cref="MtQuery{TModel, TResponse}"/>.
        /// </summary>
        /// <param name="model">Модель данных.</param>
        /// <param name="username">Наименование пользователя.</param>
        protected MtQuery(TModel model, string username = "root user")
        {
            this.Model = Check.NotNull(model, nameof(model));
            this.UserName = Check.NotNull(username, nameof(username));
            this.Guid = Guid.NewGuid();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Mt query: '{this.Guid}', username: '{this.UserName}'";
        }
    }
}
using Mt.ChangeLog.Context;
using Mt.ChangeLog.DataAccess;
using Mt.ChangeLog.Logic;
using Mt.ChangeLog.TransferObjects;
using Mt.ChangeLog.WebAPI.Infrastracture;
using System.Reflection;

namespace Mt.ChangeLog.WebAPI
{
    /// <summary>
    /// Класс инициализирующий приложение.
    /// </summary>
    public sealed class Startup
    {
        /// <inheritdoc cref="IConfiguration"/>
        public IConfiguration Configuration { get; private set; }

        /// <summary>
        /// Инициализация экземпляра класса <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration">Набор свойств конфигурации приложения.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Метод конфигурации сервисов приложения.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region [ MtChangeLog services configuration ]

            var assemblies = new Assembly[]
            {
                typeof(ModelLayer).Assembly,
                typeof(LogicLayer).Assembly,
            };

            services.AddApplicationContext();
            services.AddDataAccess();
            services.AddLogic(assemblies);

            #endregion

            #region [ Infrastracture services configuration ]

            services.AddSwaggerDocumentation();

            #endregion

            services.AddControllers(configure =>
            {
                configure.Filters.Add<ApiExceptionFilter>();
            });
            services.AddEndpointsApiExplorer();
        }

        /// <summary>
        /// Метод настройки конвейера HTTP-запросов.
        /// </summary>
        /// <param name="builder">Строитель приложения.</param>
        /// <param name="environment">Окружение приложения.</param>
        public void Configure(IApplicationBuilder builder, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                builder.UseDeveloperExceptionPage();
            }

            builder.UseRouting();
            builder.UseAuthorization();

            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            builder.UseSwaggerDocumentation();

            builder.UseDefaultDatabaseInitialization();
        }
    }
}
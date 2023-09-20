using System.Reflection;

using Mt.ChangeLog.DataAccess;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities;
using Mt.ChangeLog.Logic;
using Mt.ChangeLog.TransferObjects;
using Mt.ChangeLog.WebAPI.Infrastructure;

namespace Mt.ChangeLog.WebAPI
{
    /// <summary>
    /// Класс инициализирующий приложение.
    /// </summary>
    public sealed class Startup
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration">Набор свойств конфигурации приложения.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.Assemblies = new Assembly[]
            {
                typeof(DataAccessLayer).Assembly,
                typeof(DataContextLayer).Assembly,
                typeof(EntityLayer).Assembly,
                typeof(LogicLayer).Assembly,
                typeof(ModelLayer).Assembly,
                typeof(ServiceLayer).Assembly,
            };
        }

        /// <inheritdoc cref="IConfiguration"/>
        public IConfiguration Configuration { get; init; }

        /// <summary>
        /// Перечень сборок проекта.
        /// </summary>
        public IReadOnlyCollection<Assembly> Assemblies { get; init; }

        /// <summary>
        /// Метод конфигурации сервисов приложения.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region [ MtChangeLog services configuration ]

            services.AddApplicationContext();
            services.AddDataAccess();
            services.AddLogic(this.Assemblies);

            #endregion

            #region [ Infrastracture services configuration ]

            services.AddScoped<IMtUser, MtUser>();
            services.AddHttpContextAccessor();
            services.AddSwaggerDocumentation(this.Assemblies);

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
        public void Configure(IApplicationBuilder builder)
        {
            builder.UseRouting();
            builder.UseAuthorization();

            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger");
                    return Task.CompletedTask;
                });
            });

            builder.UseSwaggerDocumentation();
            builder.UseDefaultDatabaseInitialization();
        }
    }
}
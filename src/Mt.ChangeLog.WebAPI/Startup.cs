using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Logic;
using Mt.ChangeLog.TransferObjects;

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

            services.AddTransferObjects();
            services.AddApplicationContext(this.Configuration);
            services.AddLogic();

            #endregion

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MtChangeLog.WebAPI", Version = "v1" });
            });
        }

        /// <summary>
        /// Метод настройки конвейера HTTP-запросов.
        /// </summary>
        /// <param name="builder">Построитель приложения.</param>
        /// <param name="environment">Окружение приложения.</param>
        public void Configure(IApplicationBuilder builder, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                builder.UseDeveloperExceptionPage();
                builder.UseSwagger();
                builder.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MtChangeLog.WebAPI v1"));
            }

            builder.UseDefaultFiles();
            builder.UseStaticFiles();

            builder.UseRouting();

            builder.UseAuthorization();

            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
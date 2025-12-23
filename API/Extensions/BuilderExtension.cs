using DAL.Efcore.Data;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Categories;
using DAL.Efcore.Repositories.Clients;
using DAL.Efcore.Repositories.Orders;
using DAL.Efcore.Repositories.Producers;
using DAL.Efcore.Repositories.Products;
using DAL.Efcore.Repositories.Repository;
using DAL.Efcore.Repositories.Suppliers;
using DAL.Efcore.Repositories.UOW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    /// <summary>
    /// Вспомогательный статический класс-расширение для настройки сервисов приложения в процессе сборки.
    /// Содержит методы-расширения для регистрации объектов DAL, BLL и настройки аутентификации.
    /// </summary>
    internal static partial class BuilderExtension
    {
        /// <summary>
        /// Регистрирует объекты уровня доступа к данным (DAL) в контейнере зависимостей.
        /// </summary>
        /// <param name="builder">Экземпляр <see cref="WebApplicationBuilder"/>, используемый для конфигурации приложения.</param>
        /// <remarks>
        /// Метод извлекает строку подключения с именем <c>DefaultConnection</c> из конфигурации.
        /// Если строка подключения отсутствует — будет выброшено <see cref="InvalidOperationException"/>.
        /// Зарегистрированный контекст базы данных — <see cref="FinalProjectDbContext"/> — использует провайдер SQL Server.
        /// </remarks>
        /// <exception cref="InvalidOperationException">Выбрасывается, если строка подключения <c>DefaultConnection</c> не найдена в конфигурации.</exception>
        public static void AddDALObjects(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Строка подключения отсутствует");

            builder.Services.AddDbContext<FinalProjectDbContext>(o => o.UseSqlServer(connectionString));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.AddRepositories();
        }

        /// <summary>
        /// Регистрирует объекты уровня бизнес-логики (BLL) в контейнере зависимостей.
        /// </summary>
        /// <param name="builder">Экземпляр <see cref="WebApplicationBuilder"/>, используемый для конфигурации приложения.</param>
        /// <remarks>
        /// Метод служит точкой расширения для добавления сервисов BLL (например, сервисов доменной логики,
        /// менеджеров и интерфейсов). В текущей реализации метод оставлен пустым и должен быть заполнен
        /// по мере добавления BLL-компонентов в проект.
        /// </remarks>
        public static void AddBLLObjects(this WebApplicationBuilder builder)
        {

        }

        /// <summary>
        /// Настраивает аутентификацию и связанные с ней сервисы.
        /// </summary>
        /// <param name="builder">Экземпляр <see cref="WebApplicationBuilder"/>, используемый для конфигурации приложения.</param>
        /// <remarks>
        /// Метод предназначен для регистрации схем аутентификации, политики авторизации и конфигурации JWT/OAuth и т.д.
        /// В текущей реализации метод оставлен пустым и должен быть реализован согласно требованиям к безопасности приложения.
        /// </remarks>
        public static void AddAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidAudience = builder.Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                        };
                    });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Администратор", policy => policy.RequireRole("Администратор"));
                options.AddPolicy("Менеджер", policy => policy.RequireRole("Менеджер"));
                options.AddPolicy("Клиент", policy => policy.RequireRole("Клиент"));
            });
        }

    }
}
using DAL.Efcore.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    internal static class BuilderExtension
    {
        public static void AddDALObjects(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Строка подключения отсутствует");
            
            builder.Services.AddDbContext<FinalProjectDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        public static void AddBLLObjects(this WebApplicationBuilder builder)
        {

        }

        public static void AddAuthentication(this WebApplicationBuilder builder)
        {

        }
    }
}

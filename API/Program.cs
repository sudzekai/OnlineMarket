
using API.Extensions;
using API.Models;
using Scalar.AspNetCore;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            });

            builder.AddDALObjects();
            builder.AddBLLObjects();
            builder.AddAuthentication();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.MapScalarApiReference(options =>
                {
                    options.OpenApiRoutePattern = "/openapi/v1.json";
                    options.Title = "Online Market";

                    options.AddPreferredSecuritySchemes("Bearer").AddHttpAuthentication("Bearer", auth =>
                    {
                        auth.Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiOTRkNW91c0BnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiLQkNC00LzQuNC90LjRgdGC0YDQsNGC0L7RgCIsImV4cCI6MTc2NjIyNjU0MSwiaXNzIjoiTXlBcHAiLCJhdWQiOiJNeUF1ZGllbmNlIn0.iwFKcM-W42f3UrZ-XKYABjm57xPkcLuuV_TQOU-LN0c";
                    });
                });
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using TestTask.Api.Auth;
using TestTask.Api.Database;
using TestTask.Api.Infrastructure;
using TestTask.Api.—ached—ountries;

namespace TestTask.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();

            builder.Services.AddApiAuthentication();

            builder.Services.AddMemoryCache();
            builder.Services.AddHostedService<—ountryCacheInitializer>();
            builder.Services.AddExceptionHandler<ApiExceptionHandler>();
            builder.Services.AddScoped<ICountryFactory, CountryFactory>();
            builder.Services.AddScoped<IUserService, UserService>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            var sdfdf = app.Services.GetService<IActionDescriptorCollectionProvider>();

            app.MigrateDatabase();

            app.Run();
        }
    }
}

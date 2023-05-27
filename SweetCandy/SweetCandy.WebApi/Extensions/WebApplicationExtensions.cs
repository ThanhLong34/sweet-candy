using Microsoft.EntityFrameworkCore;
using SweetCandy.Data.Contexts;
using SweetCandy.Services.Services;

namespace SweetCandy.WebApi.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();
            builder.Services.AddDbContext<SweetCandyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICandyService, CandyService>();

            return builder;
        }

        public static WebApplicationBuilder ConfigureCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("default", policyBuilder => policyBuilder
                                                                .AllowAnyOrigin()
                                                                .AllowAnyHeader()
                                                                .AllowAnyMethod());
            });

            return builder;
        }

        public static WebApplicationBuilder ConfigureSwaggerOpenApi(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder;
        }

        public static WebApplication SetupRequestPipeLine(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseCors("default");

            return app;
        }
    }
}

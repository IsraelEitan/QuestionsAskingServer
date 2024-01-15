
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;
using QuestionsAskingServer.Data;
using QuestionsAskingServer.Validators;
using QuestionsAskingServer.Repositories;
using QuestionsAskingServer.Services;
using QuestionsAskingServer.Helpers;
using QuestionsAskingServer.Repositories.Interfaces;

namespace QuestionsAskingServer.Extensions
{
    internal static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IQuestionsService, QuestionsService>();
            services.AddScoped<ICacheService, QASCacheService>();
            services.AddScoped<IUnitOfWork, QuestionsUnitOfWork>();
            services.AddScoped<IQuestionRepository, QuestionsRepository>();

            services.AddDbContext<QASDBContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(Program).Assembly.FullName)));

            services.AddAutoMapper(typeof(Program));

            services.AddControllers(options =>
            {
                options.Filters.Add(new ModelValidationActionFilter());
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "QuestionsAskingServer Web App API",
                    Description = "QuestionsAskingServer Web App API (ASP.NET Core 7.0)",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Add memory cache if not already added
            if (!services.Any(x => x.ServiceType == typeof(IMemoryCache)))
            {
                services.AddMemoryCache();
            }

            services.AddEndpointsApiExplorer();
        }
    }
}



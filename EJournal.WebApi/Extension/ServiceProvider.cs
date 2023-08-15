using EJournal.Application;
using EJournal.MongoPersistence;
using EJournal.WebApi.Extension.BackgroundServices.CreatorWeeklySchedule;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace EJournal.WebApi.Extension;

public static class ServiceProvider
{
    public static void AddServices(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo {Title = "Demo API", Version = "v1"});
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);
        services.AddCors((opt) =>
        {
            opt.AddDefaultPolicy(builder =>
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
            );
        });
        services.AddHostedService<CreatorNewWeeklySchedule>();
        services.AddScoped<ICreateWeeklyScheduleProcess, CreateWeeklyScheduleProcess>();
        services.AddJwtAuthorization(cfg);
        services.AddApplicationServices();
        services.UsePersistence(cfg);
    }
}
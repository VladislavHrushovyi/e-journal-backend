using EJournal.Application;
using EJournal.MongoPersistence;
using Microsoft.AspNetCore.Mvc;

namespace EJournal.WebApi.Extension;

public static class ServiceProvider
{
    public static void AddServices(this IServiceCollection services, IConfiguration cgf)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);
        services.AddCors((opt) =>
        {
            opt.AddDefaultPolicy(builder =>
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
            );
        });
        services.AddApplicationServices();
        services.UsePersistence(cgf);
    }
}
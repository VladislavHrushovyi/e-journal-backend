using EJournal.Application.Repositories;
using EJournal.MongoPersistence.Context;
using EJournal.MongoPersistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace EJournal.MongoPersistence;

public static class Extension
{
    public static IServiceCollection UsePersistence(this IServiceCollection services, IConfiguration cfg)
    {
        var dbSettings = cfg.GetSection("EJournalMongo");
        services.Configure<MongoDbSettings>(dbSettings);
        services.AddScoped<DataContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWorkDayRepository, WorkDayRepository>();
        services.AddScoped<IWeeklyScheduleRepository, WeeklyScheduleRepository>();
        services.AddScoped<BaseUnitOfWork, UnitOfWork>();
        return services;
    }
}
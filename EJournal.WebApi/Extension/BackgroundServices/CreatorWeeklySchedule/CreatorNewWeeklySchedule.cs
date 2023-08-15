using MediatR;

namespace EJournal.WebApi.Extension.BackgroundServices.CreatorWeeklySchedule;

public class CreatorNewWeeklySchedule : BackgroundService
{
    private IServiceProvider services;

    public CreatorNewWeeklySchedule(IServiceProvider services)
    {
        this.services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        using var scope = services.CreateScope();
        var scopedWeeklyScheduleCreatorProcess =
            scope.ServiceProvider.GetRequiredService<ICreateWeeklyScheduleProcess>();

        await scopedWeeklyScheduleCreatorProcess.DoWork(stoppingToken);
    }
}
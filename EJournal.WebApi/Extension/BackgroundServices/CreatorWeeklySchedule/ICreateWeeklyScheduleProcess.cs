namespace EJournal.WebApi.Extension.BackgroundServices.CreatorWeeklySchedule;

public interface ICreateWeeklyScheduleProcess
{
    public Task DoWork(CancellationToken stoppingToken);
}
namespace EJournal.Application.Repositories;

public class BaseUnitOfWork
{
    public readonly IUserRepository _userRepository;
    public readonly IWorkDayRepository _WorkDayRepository;
    public readonly IWeeklyScheduleRepository WeeklyScheduleRepository;

    public BaseUnitOfWork(IWeeklyScheduleRepository weeklyScheduleRepository, IWorkDayRepository workDayRepository, IUserRepository userRepository)
    {
        WeeklyScheduleRepository = weeklyScheduleRepository;
        _WorkDayRepository = workDayRepository;
        _userRepository = userRepository;
    }
}
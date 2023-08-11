namespace EJournal.Application.Repositories;

public class BaseUnitOfWork
{
    public readonly IUserRepository _userRepository;
    public readonly IWorkDayRepository _WorkDayRepository;
    public readonly IWorkTimeRepository _WorkTimeRepository;

    public BaseUnitOfWork(IWorkTimeRepository workTimeRepository, IWorkDayRepository workDayRepository, IUserRepository userRepository)
    {
        _WorkTimeRepository = workTimeRepository;
        _WorkDayRepository = workDayRepository;
        _userRepository = userRepository;
    }
}
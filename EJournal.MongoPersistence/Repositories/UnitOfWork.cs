using EJournal.Application.Repositories;
using EJournal.MongoPersistence.Context;

namespace EJournal.MongoPersistence.Repositories;

public sealed class UnitOfWork
{
    public readonly IUserRepository _userRepository;
    public readonly IWorkDayRepository _WorkDayRepository;
    public readonly IWorkTimeRepository _WorkTimeRepository;
    public readonly DataContext _DataContext;

    public UnitOfWork(IUserRepository userRepository, IWorkDayRepository workDayRepository, IWorkTimeRepository workTimeRepository, DataContext dataContext)
    {
        _userRepository = userRepository;
        _WorkDayRepository = workDayRepository;
        _WorkTimeRepository = workTimeRepository;
        _DataContext = dataContext;
    }
}
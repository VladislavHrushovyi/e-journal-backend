using EJournal.Application.Repositories;

namespace EJournal.MongoPersistence.Repositories;

public sealed class UnitOfWork
{
    public readonly IUserRepository _userRepository;
    public readonly IWorkDayRepository _WorkDayRepository;
    public readonly IWorkTimeRepository _WorkTimeRepository;

    public UnitOfWork(IUserRepository userRepository, IWorkDayRepository workDayRepository, IWorkTimeRepository workTimeRepository)
    {
        _userRepository = userRepository;
        _WorkDayRepository = workDayRepository;
        _WorkTimeRepository = workTimeRepository;
    }

    public async Task Save(CancellationToken cts)
    {
        throw new NotImplementedException();
    }
}
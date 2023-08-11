using EJournal.Application.Repositories;
using EJournal.MongoPersistence.Context;

namespace EJournal.MongoPersistence.Repositories;

public sealed class UnitOfWork : BaseUnitOfWork
{
    public UnitOfWork(IWorkTimeRepository workTimeRepository, IWorkDayRepository workDayRepository, IUserRepository userRepository) : base(workTimeRepository, workDayRepository, userRepository)
    {
    }
}
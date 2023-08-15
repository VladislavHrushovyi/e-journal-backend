using EJournal.Application.Repositories;
using EJournal.MongoPersistence.Context;

namespace EJournal.MongoPersistence.Repositories;

public sealed class UnitOfWork : BaseUnitOfWork
{
    public UnitOfWork(IWeeklyScheduleRepository weeklyScheduleRepository, IWorkDayRepository workDayRepository, IUserRepository userRepository) : base(weeklyScheduleRepository, workDayRepository, userRepository)
    {
    }
}
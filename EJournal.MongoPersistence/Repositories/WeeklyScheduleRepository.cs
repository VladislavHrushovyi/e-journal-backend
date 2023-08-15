using EJournal.Application.Repositories;
using EJournal.Domain.Entities;
using EJournal.MongoPersistence.Context;

namespace EJournal.MongoPersistence.Repositories;

public sealed class WeeklyScheduleRepository : BaseRepository<WeeklySchedule>, IWeeklyScheduleRepository
{
    public WeeklyScheduleRepository(DataContext dataContext) : base(dataContext)
    {
    }
}
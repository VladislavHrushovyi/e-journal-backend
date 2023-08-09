using EJournal.Application.Repositories;
using EJournal.Domain.Entities;
using EJournal.MongoPersistence.Context;

namespace EJournal.MongoPersistence.Repositories;

public sealed class WorkDayRepository : BaseRepository<WorkDay>, IWorkDayRepository
{
    public WorkDayRepository(DataContext dataContext) : base(dataContext)
    {
    }
}
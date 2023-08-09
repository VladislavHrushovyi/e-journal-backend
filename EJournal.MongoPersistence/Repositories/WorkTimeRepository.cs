using EJournal.Application.Repositories;
using EJournal.Domain.Entities;
using EJournal.MongoPersistence.Context;

namespace EJournal.MongoPersistence.Repositories;

public sealed class WorkTimeRepository : BaseRepository<WorkTime>, IWorkTimeRepository
{
    public WorkTimeRepository(DataContext dataContext) : base(dataContext)
    {
    }
}
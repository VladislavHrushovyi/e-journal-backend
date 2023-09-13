using EJournal.Domain.Common;
using EJournal.Domain.Entities;

namespace EJournal.Application.Repositories;

public interface IWorkDayRepository : IBaseRepository<WorkDay>
{
    public Task<WorkDay> GetByDayOfWeek(CustomDayOfWeek customDayOfWeek, CancellationToken ct);
}
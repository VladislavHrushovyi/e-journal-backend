﻿using EJournal.Application.Repositories;
using EJournal.Domain.Common;
using EJournal.Domain.Entities;
using EJournal.MongoPersistence.Context;
using MongoDB.Driver;

namespace EJournal.MongoPersistence.Repositories;

public sealed class WorkDayRepository : BaseRepository<WorkDay>, IWorkDayRepository
{
    public WorkDayRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public async Task<WorkDay> GetByDayOfWeek(CustomDayOfWeek customDayOfWeek, CancellationToken ct)
    {
        var day = await _collection.FindAsync(d => d.DayOfWeek == customDayOfWeek, cancellationToken: ct);

        return day.FirstOrDefault(cancellationToken: ct);
    }
}
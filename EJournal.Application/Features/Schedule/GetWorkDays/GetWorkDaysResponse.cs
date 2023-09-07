using EJournal.Domain.Entities;

namespace EJournal.Application.Features.Schedule.GetWorkDays;

public sealed class GetWorkDaysResponse
{
    public GetWorkDaysResponse(IEnumerable<WorkDay> workDays)
    {
        WorkDays = workDays;
    }

    public IEnumerable<WorkDay> WorkDays { get; set; }
}
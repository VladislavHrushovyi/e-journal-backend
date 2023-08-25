using EJournal.Domain.Entities;

namespace EJournal.Application.Features.Schedule.GetActualWeeklySchedule;

public sealed class GetActualWeeklyScheduleResponse
{
    public WeeklySchedule ActualWeeklySchedule { get; set; }
}
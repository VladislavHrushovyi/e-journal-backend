using EJournal.Domain.Common;

namespace EJournal.Domain.Entities;

public class WorkDay : BaseEntity
{
    public bool IsWorkDay { get; set; } = false;
    public CustomDayOfWeek DayOfWeek { get; set; }
    public IEnumerable<WorkTime> Times { get; set; } = Enumerable.Empty<WorkTime>();
}
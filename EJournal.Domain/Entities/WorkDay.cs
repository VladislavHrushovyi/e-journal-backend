using EJournal.Domain.Common;

namespace EJournal.Domain.Entities;

public class WorkDay : BaseEntity
{
    public bool IsWorkDay { get; set; } = false;
    public DayOfWeek DayOfWeek { get; set; }
    public List<WorkTime>? Times { get; set; }
}
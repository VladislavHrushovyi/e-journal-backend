using EJournal.Domain.Common;

namespace EJournal.Domain.Entities;

public sealed class WeeklySchedule : BaseEntity
{
    public string WeekId { get; set; }
    public IEnumerable<WorkDay> WorkDays { get; set; }
}
using EJournal.Domain.Common;

namespace EJournal.Domain.Entities;

public class RecordHistoryItem : BaseEntity
{
    public Guid WorkTimeId { get; set; }
    public DateTime Date { get; set; }
    public CustomDayOfWeek DayOfWeek { get; set; }
    public ReservationStatus Status { get; set; }
}
using EJournal.Domain.Common;

namespace EJournal.Domain.Entities;

public class WorkTime : BaseEntity
{
    public DateTime Time { get; set; }
    public ReservationStatus Status { get; set; }
    public Guid UserId { get; set; }
}
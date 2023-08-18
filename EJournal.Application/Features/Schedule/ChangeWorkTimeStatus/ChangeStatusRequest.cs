using EJournal.Domain.Common;
using MediatR;

namespace EJournal.Application.Features.Schedule.ChangeWorkTimeStatus;

public sealed class ChangeStatusRequest : IRequest<ChangeStatusResponse>
{
    public CustomDayOfWeek DayOfWeek { get; set; }
    public ReservationStatus Status { get; set; }
    public Guid WorkTimeId { get; set; }
}
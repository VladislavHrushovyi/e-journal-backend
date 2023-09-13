using EJournal.Domain.Common;
using MediatR;

namespace EJournal.Application.Features.Schedule.DeleteWorkTimeFromWorkDay;

public sealed class DeleteWorkTimeFromWorkDayRequest : IRequest<DeleteWorkTimeFromWorkDayResponse>
{
    public Guid WorkTimeId { get; set; }
    public CustomDayOfWeek DayOfWeek { get; set; }
}
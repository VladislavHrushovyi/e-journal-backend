using EJournal.Domain.Common;
using MediatR;

namespace EJournal.Application.Features.Schedule.AddTimeToWorkDay;

public sealed class AddTimeToWorkDayRequest : IRequest<AddTimeToWorkDayResponse>
{
    public string Time { get; set; }
    public CustomDayOfWeek DayOfWeek { get; set; }
}
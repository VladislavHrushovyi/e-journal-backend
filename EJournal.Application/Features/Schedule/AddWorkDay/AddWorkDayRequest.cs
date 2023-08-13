using EJournal.Domain.Common;
using MediatR;

namespace EJournal.Application.Features.Schedule.AddWorkDay;

public sealed class AddWorkDayRequest : IRequest<AddWorkDayResponse>
{
    public bool IsWorkDay { get; set; }
    public CustomDayOfWeek DayOfWeek { get; set; }
}
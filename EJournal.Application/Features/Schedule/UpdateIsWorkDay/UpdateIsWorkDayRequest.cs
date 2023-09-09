using EJournal.Domain.Entities;
using MediatR;

namespace EJournal.Application.Features.Schedule.UpdateIsWorkDay;

public sealed class UpdateIsWorkDayRequest : IRequest<UpdateIsWorkDayResponse>
{
    public string WorkDayId { get; set; }
    public bool IsWorkDay { get; set; }
}
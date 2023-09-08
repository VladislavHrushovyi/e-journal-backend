using EJournal.Domain.Entities;
using MediatR;

namespace EJournal.Application.Features.Schedule.UpdateWorkDayConfig;

public sealed class UpdateWorkDayConfigRequest : IRequest<UpdateWorkDayConfigResponse>
{
    public WorkDay WorkDay { get; set; }
}
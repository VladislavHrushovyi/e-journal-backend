using EJournal.Application.Features.Schedule.AddTimeToWorkDay;
using EJournal.Application.Features.Schedule.AddWorkDay;
using MediatR;

namespace EJournal.WebApi.Extension.Endpoints;

public static class ScheduleEndpoints
{
    public static RouteGroupBuilder UseScheduleEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/add-work-day", async (AddWorkDayRequest req, IMediator mediatr, CancellationToken ct)
            => await mediatr.Send(req, ct));
        group.MapPost("/add-time-to-work-day",
            async (AddTimeToWorkDayRequest req, IMediator mediator, CancellationToken ct)
                => await mediator.Send(req, ct));
        return group;
    }
}
using EJournal.Application.Features.Schedule.AddWorkDay;
using MediatR;

namespace EJournal.WebApi.Extension.Endpoints;

public static class ScheduleEndpoints
{
    public static RouteGroupBuilder UseScheduleEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/add-work-day", async (AddWorkDayRequest req, IMediator mediatr, CancellationToken ct)
         => await mediatr.Send(req, ct));
        return group;
    }
}
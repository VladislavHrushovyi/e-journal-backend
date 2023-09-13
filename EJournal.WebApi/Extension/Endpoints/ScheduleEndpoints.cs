using EJournal.Application.Features.Schedule.AddTimeToWorkDay;
using EJournal.Application.Features.Schedule.AddWorkDay;
using EJournal.Application.Features.Schedule.ChangeWorkTimeStatus;
using EJournal.Application.Features.Schedule.DeleteWorkTimeFromWorkDay;
using EJournal.Application.Features.Schedule.GetActiveRecords;
using EJournal.Application.Features.Schedule.GetActualWeeklySchedule;
using EJournal.Application.Features.Schedule.GetWorkDays;
using EJournal.Application.Features.Schedule.UpdateIsWorkDay;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EJournal.WebApi.Extension.Endpoints;

public static class ScheduleEndpoints
{
    public static RouteGroupBuilder UseScheduleEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/add-work-day", [Authorize] async (AddWorkDayRequest req, IMediator mediatr, CancellationToken ct)
            => await mediatr.Send(req, ct));
        group.MapPost("/add-time-to-work-day",
            [Authorize] async (AddTimeToWorkDayRequest req, IMediator mediator, CancellationToken ct)
                => await mediator.Send(req, ct));
        group.MapPost("/update-work-time-status", [Authorize] async (ChangeStatusRequest req, IMediator mediator, CancellationToken ct) 
            => await mediator.Send(req, ct));
        group.MapGet("/actual-schedule", [Authorize] async (IMediator mediator, CancellationToken ct)
            => await mediator.Send(new GetActualWeeklyScheduleRequest(), ct));
        group.MapGet("/active-record", [Authorize] async (IMediator mediator, CancellationToken ct)
            => await mediator.Send(new GetActiveRecordsRequest(), ct));
        group.MapGet("/work-days", [Authorize] async (IMediator mediator, CancellationToken ct)
            => await mediator.Send(new GetWorkDaysRequest(), ct));
        group.MapPatch("/set-work-day", async (UpdateIsWorkDayRequest req, IMediator mediator, CancellationToken ct)
            => await mediator.Send(req, ct));
        group.MapPatch("/delete-time", [Authorize] async ([FromBody]DeleteWorkTimeFromWorkDayRequest req, IMediator mediator, CancellationToken ct)
            => await mediator.Send(req, ct));
        return group;
    }
}
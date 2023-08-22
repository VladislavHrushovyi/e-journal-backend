using System.Security.Claims;
using EJournal.Application.Features.User.Login;
using EJournal.Application.Features.User.RecordingToTimeWork;
using EJournal.Application.Features.User.Register;
using EJournal.Application.Features.User.UpdateInformation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EJournal.WebApi.Extension.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder AddUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/register", async (UserRegisterRequest req, IMediator mediator, CancellationToken ct)
            => await mediator.Send(req, ct));
        group.MapPost("/login", async (UserLoginRequest req, IMediator mediator, CancellationToken ct)
            => await mediator.Send(req, ct));
        group.MapPost("/recording-to-work-time", [Authorize] async
        (
            HttpContext context,
            [FromBody] RecordingToTimeWorkRequest req,
            IMediator mediator, CancellationToken ct
        ) =>
        {
            var userIdString = context.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userGuid = Guid.Parse(userIdString);
            req.UserId = userGuid;
            await mediator.Send(req, ct);
        });
        group.MapPatch("/update-information",
            async (
                    HttpContext context,
                    UpdateInformationRequest req, 
                    IMediator mediator, 
                    CancellationToken ct)
                =>
            {
                var userIdString = context.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var userGuid = Guid.Parse(userIdString);
                req.UserId = userGuid;
                await mediator.Send(req, ct);
            });
        group.MapGet("/hello", [Authorize] async () => "Authorize");
        return group;
    }
}
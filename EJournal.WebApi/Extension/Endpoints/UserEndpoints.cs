using EJournal.Application.Features.User.Login;
using EJournal.Application.Features.User.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace EJournal.WebApi.Extension.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder AddUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/register", async (UserRegisterRequest req, IMediator mediator, CancellationToken ct)
            => await mediator.Send(req, ct));
        group.MapPost("/login", async (UserLoginRequest req, IMediator mediator, CancellationToken ct)
            => await mediator.Send(req, ct));
        group.MapGet("/hello", [Authorize] async () => "Authorize");
        return group;
    }
}
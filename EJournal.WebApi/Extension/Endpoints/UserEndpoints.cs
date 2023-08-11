using EJournal.Application.Features.User.Register;
using MediatR;

namespace EJournal.WebApi.Extension.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder AddUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/register", async (UserRegisterRequest req, IMediator mediator, CancellationToken ct) 
            => await mediator.Send(req, ct));
        return group;
    }
}
using EJournal.MongoPersistence.Context;
using EJournal.MongoPersistence.Repositories;

namespace EJournal.WebApi.Extension.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder AddUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", (UnitOfWork unit) => unit);
        group.MapPost("/register", (DataContext context) => context.Users);
        return group;
    }
}
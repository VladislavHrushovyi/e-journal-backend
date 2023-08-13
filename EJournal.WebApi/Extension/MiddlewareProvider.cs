using EJournal.WebApi.Extension.Endpoints;

namespace EJournal.WebApi.Extension;

public static class MiddlewareProvider
{
    public static void UseMiddlewares(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapGroup("/user")
            .AddUserEndpoints()
            .WithTags("UserEndpoints");
        app.MapGroup("/schedule")
            .UseScheduleEndpoints()
            .WithTags("ScheduleEndpoints");

        app.UseAuthentication();
        app.UseAuthorization();
        app.ExceptionHandler();
    }
}
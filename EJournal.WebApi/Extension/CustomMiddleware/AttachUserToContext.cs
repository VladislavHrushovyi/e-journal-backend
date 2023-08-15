using System.Security.Claims;

namespace EJournal.WebApi.Extension.CustomMiddleware;

public static class AttachUserToContext
{
    public static void UseAttachUserToContext(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            if (context.Request.Path.Value == "/user/login" || context.Request.Path.Value == "/user/register")
            {
                   
            }
            else
            {
                var authToken = context.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(authToken))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Auth token doesnt exist, please login");
                }
            
                var userData = new
                {
                    Id = context.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
                    FirstName = context.User.Claims.First(c => c.Type == ClaimTypes.Name).Value,
                    LastName = context.User.Claims.First(c => c.Type == ClaimTypes.Surname).Value,
                    PhoneNumber = context.User.Claims.First(c => c.Type == ClaimTypes.MobilePhone).Value
                };
            
                context.Items.Add("User", userData);
            }
            await next.Invoke();
        });
    }
}
using EJournal.WebApi.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();
app.UseMiddlewares();
app.Run();
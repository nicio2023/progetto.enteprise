using Paradigmi.Progetto.Application.Extensions;
using Paradigmi.Progetto.Models.Extensions;
using Paradigmi.Progetto.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services
    .AddWebServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddModelServices(builder.Configuration);

var app = builder.Build();


app.AddWebMiddleware();


app.Run();

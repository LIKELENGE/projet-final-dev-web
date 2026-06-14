using Api.EndPoints;
using Api.Middleware;
using Core;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapGet("/", () => "API Marketplace");
app.MapUtilisateurEndpoints();
app.MapAnnonceEndpoints();
app.MapCategorieEndpoints();
app.MapConversationEndpoints();
app.MapModerationEndpoints();

app.Run();

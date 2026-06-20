using Api.EndPoints;
using Api.Middleware;
using Api.Security;
using Core;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("Frontend");
app.UseStaticFiles();

app.MapGet("/", () => "API Marketplace");
app.MapUtilisateurEndpoints();
app.MapAnnonceEndpoints();
app.MapImageEndpoints();
app.MapCategorieEndpoints();
app.MapConversationEndpoints();
app.MapModerationEndpoints();
app.MapChoixEndpoints();

app.Run();

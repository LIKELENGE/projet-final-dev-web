using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class ChoixEndpoints
{
    public static IEndpointRouteBuilder MapChoixEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/choix");

        group.MapGet("/categories", (IListerChoixUseCase useCase) =>
        {
            return Results.Ok(useCase.GetCategories());
        });

        group.MapGet("/communes", (IListerChoixUseCase useCase) =>
        {
            return Results.Ok(useCase.GetCommunes());
        });

        group.MapGet("/etats-annonce", (IListerChoixUseCase useCase) =>
        {
            return Results.Ok(useCase.GetEtatsAnnonce());
        });

        group.MapGet("/sexes", (IListerChoixUseCase useCase) =>
        {
            return Results.Ok(useCase.GetSexes());
        });

        return app;
    }
}

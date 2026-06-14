using Api.Models;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class CategorieEndpoints
{
    public static IEndpointRouteBuilder MapCategorieEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/categories");

        group.MapPost("/", (CreerCategorieRequest request, ICreerCategorieUseCase useCase) =>
        {
            var categorie = new Categorie
            {
                Nom = request.Nom
            };

            useCase.Execute(categorie);

            return Results.Created("/categories", categorie);
        });

        group.MapPut("/{categorieId:int}", (int categorieId, ModifierCategorieRequest request, IModifierCategorieUseCase useCase) =>
        {
            var categorie = new Categorie
            {
                Id = categorieId,
                Nom = request.Nom
            };

            useCase.Execute(categorie);

            return Results.NoContent();
        });

        group.MapDelete("/{categorieId:int}", (int categorieId, ISupprimerCategorieUseCase useCase) =>
        {
            useCase.Execute(categorieId);

            return Results.NoContent();
        });

        return app;
    }
}

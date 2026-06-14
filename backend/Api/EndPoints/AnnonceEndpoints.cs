using Api.Models;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class AnnonceEndpoints
{
    public static IEndpointRouteBuilder MapAnnonceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/annonces");

        group.MapGet("/", (string? recherche, int? categorieId, IRechercherAnnonceUseCase useCase) =>
        {
            return Results.Ok(useCase.Execute(recherche, categorieId));
        });

        group.MapGet("/{annonceId:int}", (int annonceId, IConsulterAnnonceUseCase useCase) =>
        {
            var annonce = useCase.Execute(annonceId);

            return annonce == null ? Results.NotFound() : Results.Ok(annonce);
        });

        group.MapPost("/", (CreerAnnonceRequest request, ICreerAnnonceUseCase useCase) =>
        {
            var annonce = MapAnnonce(request);

            useCase.Execute(annonce);

            return Results.Created("/annonces", annonce);
        });

        group.MapPut("/{annonceId:int}", (int annonceId, ModifierAnnonceRequest request, IModifierAnnonceUseCase useCase) =>
        {
            var annonce = new Annonce
            {
                Id = annonceId,
                Utilisateur = new Utilisateur { Id = request.UtilisateurId },
                Categorie = new Categorie { Id = request.CategorieId },
                Commune = request.CommuneId.HasValue ? new Commune { Id = request.CommuneId.Value } : null,
                Nom = request.Nom,
                Description = request.Description,
                Prix = request.Prix
            };

            useCase.Execute(annonce);

            return Results.NoContent();
        });

        group.MapDelete("/{annonceId:int}", (int annonceId, ISupprimerAnnonceUseCase useCase) =>
        {
            useCase.Execute(annonceId);

            return Results.NoContent();
        });

        return app;
    }

    private static Annonce MapAnnonce(CreerAnnonceRequest request)
    {
        return new Annonce
        {
            Utilisateur = new Utilisateur { Id = request.UtilisateurId },
            Categorie = new Categorie { Id = request.CategorieId },
            Commune = request.CommuneId.HasValue ? new Commune { Id = request.CommuneId.Value } : null,
            Nom = request.Nom,
            Description = request.Description,
            Prix = request.Prix
        };
    }
}

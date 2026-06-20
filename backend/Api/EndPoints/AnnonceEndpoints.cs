using Api.Models;
using Api.Security;
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

        group.MapGet("/mes-annonces", (HttpContext context, IConsulterMesAnnoncesUseCase useCase, IJwtTokenService jwtTokenService) =>
        {
            var utilisateurId = GetUtilisateurConnecteId(context, jwtTokenService);

            if (utilisateurId == null)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(useCase.Execute(utilisateurId.Value));
        });

        group.MapPost("/", (CreerAnnonceRequest request, HttpContext context, ICreerAnnonceUseCase useCase, IGererPhotosAnnonceUseCase photosUseCase, IJwtTokenService jwtTokenService) =>
        {
            var utilisateurId = GetUtilisateurConnecteId(context, jwtTokenService);

            if (utilisateurId == null)
            {
                return Results.Unauthorized();
            }

            var annonce = MapAnnonce(request, utilisateurId.Value);

            useCase.Execute(annonce);

            foreach (var photoRequest in request.Photos.Where(photo => !string.IsNullOrWhiteSpace(photo.Lien)))
            {
                var photo = MapPhoto(annonce.Id, photoRequest);

                photosUseCase.Ajouter(photo, utilisateurId.Value);
                annonce.Photos.Add(photo);
            }

            return Results.Created("/annonces", annonce);
        });

        group.MapGet("/{annonceId:int}/photos", (int annonceId, IGererPhotosAnnonceUseCase useCase) =>
        {
            return Results.Ok(useCase.ListerParAnnonce(annonceId));
        });

        group.MapPost("/{annonceId:int}/photos", (int annonceId, PhotoAnnonceRequest request, HttpContext context, IGererPhotosAnnonceUseCase useCase, IJwtTokenService jwtTokenService) =>
        {
            var utilisateurId = GetUtilisateurConnecteId(context, jwtTokenService);

            if (utilisateurId == null)
            {
                return Results.Unauthorized();
            }

            var photo = MapPhoto(annonceId, request);

            useCase.Ajouter(photo, utilisateurId.Value);

            return Results.Created($"/annonces/{annonceId}/photos", photo);
        });

        group.MapPut("/{annonceId:int}", (int annonceId, ModifierAnnonceRequest request, HttpContext context, IModifierAnnonceUseCase useCase, IJwtTokenService jwtTokenService) =>
        {
            var utilisateurId = GetUtilisateurConnecteId(context, jwtTokenService);

            if (utilisateurId == null)
            {
                return Results.Unauthorized();
            }

            var annonce = MapAnnonce(annonceId, request, utilisateurId.Value);

            useCase.Execute(annonce);

            return Results.NoContent();
        });

        group.MapDelete("/{annonceId:int}", (int annonceId, HttpContext context, ISupprimerAnnonceUseCase useCase, IJwtTokenService jwtTokenService) =>
        {
            var utilisateurId = GetUtilisateurConnecteId(context, jwtTokenService);

            if (utilisateurId == null)
            {
                return Results.Unauthorized();
            }

            useCase.Execute(annonceId, utilisateurId.Value);

            return Results.NoContent();
        });

        return app;
    }

    private static int? GetUtilisateurConnecteId(HttpContext context, IJwtTokenService jwtTokenService)
    {
        return jwtTokenService.GetUtilisateurId(context.Request.Headers.Authorization.ToString());
    }

    private static Annonce MapAnnonce(CreerAnnonceRequest request, int utilisateurId)
    {
        return new Annonce
        {
            Utilisateur = new Utilisateur { Id = utilisateurId },
            Categorie = new Categorie { Id = request.CategorieId },
            Commune = request.CommuneId.HasValue ? new Commune { Id = request.CommuneId.Value } : null,
            Nom = request.Nom,
            Description = request.Description,
            Prix = request.Prix
        };
    }

    private static Annonce MapAnnonce(int annonceId, ModifierAnnonceRequest request, int utilisateurId)
    {
        return new Annonce
        {
            Id = annonceId,
            Utilisateur = new Utilisateur { Id = utilisateurId },
            Categorie = new Categorie { Id = request.CategorieId },
            Commune = request.CommuneId.HasValue ? new Commune { Id = request.CommuneId.Value } : null,
            Nom = request.Nom,
            Description = request.Description,
            Prix = request.Prix
        };
    }

    private static PhotoAnnonce MapPhoto(int annonceId, PhotoAnnonceRequest request)
    {
        return new PhotoAnnonce
        {
            Annonce = new Annonce { Id = annonceId },
            Titre = request.Titre,
            Lien = request.Lien
        };
    }
}

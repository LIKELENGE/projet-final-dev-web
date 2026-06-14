using Api.Models;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class ModerationEndpoints
{
    public static IEndpointRouteBuilder MapModerationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/moderations");

        group.MapPost("/annonces/validation", (ValiderAnnonceRequest request, IValiderAnnonceUseCase useCase) =>
        {
            var moderation = new Moderer
            {
                Admin = new Admin { Compte = request.AdminCompte },
                Annonce = new Annonce { Id = request.AnnonceId },
                EtatAnnonce = new EtatAnnonce { Id = request.EtatAnnonceId },
                DelaiStatut = request.DelaiStatut,
                Illimite = request.Illimite
            };

            useCase.Execute(moderation);

            return Results.Created("/moderations/annonces/validation", moderation);
        });

        return app;
    }
}

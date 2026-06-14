using Api.Models;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class UtilisateurEndpoints
{
    public static IEndpointRouteBuilder MapUtilisateurEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/utilisateurs");

        group.MapPost("/inscription", (InscriptionRequest request, IInscrireUtilisateurUseCase useCase) =>
        {
            var utilisateur = new Utilisateur
            {
                Nom = request.Nom,
                Prenom = request.Prenom,
                Mail = request.Mail,
                Tel = request.Tel,
                PhotoProfil = request.PhotoProfil,
                MotDePasseHash = request.MotDePasseHash,
                DateNaissance = request.DateNaissance,
                Sexe = request.CodeSexe.HasValue ? new Sexe { Code = request.CodeSexe.Value } : null,
                Commune = request.CommuneId.HasValue ? new Commune { Id = request.CommuneId.Value } : null
            };

            useCase.Execute(utilisateur);

            return Results.Created("/utilisateurs", utilisateur);
        });

        return app;
    }
}

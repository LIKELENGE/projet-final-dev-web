using Api.Models;
using Api.Security;
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
                MotDePasseHash = request.MotDePasse,
                DateNaissance = request.DateNaissance,
                Sexe = request.CodeSexe is > 0 ? new Sexe { Code = request.CodeSexe.Value } : null,
                Commune = request.CommuneId is > 0 ? new Commune { Id = request.CommuneId.Value } : null
            };

            useCase.Execute(utilisateur);

            return Results.Created("/utilisateurs", utilisateur);
        });

        group.MapPost("/connexion", (ConnexionRequest request, IConnecterUtilisateurUseCase useCase, IJwtTokenService jwtTokenService) =>
        {
            var utilisateur = useCase.Execute(request.Mail, request.MotDePasse);

            return Results.Ok(new UtilisateurConnecteResponse
            {
                Id = utilisateur.Id,
                Nom = utilisateur.Nom,
                Prenom = utilisateur.Prenom,
                Mail = utilisateur.Mail,
                Token = jwtTokenService.GenerateToken(utilisateur)
            });
        });

        return app;
    }
}

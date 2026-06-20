using Core.Models;

namespace Api.Security;

public interface IJwtTokenService
{
    string GenerateToken(Utilisateur utilisateur);
    int? GetUtilisateurId(string? authorizationHeader);
}

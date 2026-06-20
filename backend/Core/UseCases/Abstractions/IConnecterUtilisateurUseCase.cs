using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IConnecterUtilisateurUseCase
{
    Utilisateur Execute(string mail, string motDePasse);
}

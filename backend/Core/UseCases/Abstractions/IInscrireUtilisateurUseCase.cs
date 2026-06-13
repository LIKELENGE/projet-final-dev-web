using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IInscrireUtilisateurUseCase
{
    void Execute(Utilisateur utilisateur);
}

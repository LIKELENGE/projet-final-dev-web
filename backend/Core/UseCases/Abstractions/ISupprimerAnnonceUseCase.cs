namespace Core.UseCases.Abstractions;

public interface ISupprimerAnnonceUseCase
{
    void Execute(int annonceId, int utilisateurId);
}

using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IGererPhotosAnnonceUseCase
{
    IEnumerable<PhotoAnnonce> ListerParAnnonce(int annonceId);
    void Ajouter(PhotoAnnonce photoAnnonce, int utilisateurId);
}

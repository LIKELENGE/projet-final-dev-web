using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IRechercherAnnonceUseCase
{
    IEnumerable<Annonce> Execute(string? termeRecherche = null, int? categorieId = null);
}

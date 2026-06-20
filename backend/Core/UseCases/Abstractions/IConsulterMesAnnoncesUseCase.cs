using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IConsulterMesAnnoncesUseCase
{
    IEnumerable<Annonce> Execute(int utilisateurId);
}

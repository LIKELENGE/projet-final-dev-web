using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IConsulterAnnonceUseCase
{
    Annonce? Execute(int annonceId);
}

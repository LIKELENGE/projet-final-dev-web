using Core.Models;

namespace Core.UseCases.Abstractions;

public interface ICreerAnnonceUseCase
{
    void Execute(Annonce annonce);
}

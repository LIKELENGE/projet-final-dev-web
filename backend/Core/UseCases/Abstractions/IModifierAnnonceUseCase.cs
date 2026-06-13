using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IModifierAnnonceUseCase
{
    void Execute(Annonce annonce);
}

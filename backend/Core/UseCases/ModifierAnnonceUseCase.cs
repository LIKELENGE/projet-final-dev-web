using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class ModifierAnnonceUseCase : IModifierAnnonceUseCase
{
    private readonly IAnnonceGateway _annonceGateway;

    public ModifierAnnonceUseCase(IAnnonceGateway annonceGateway)
    {
        _annonceGateway = annonceGateway ?? throw new ArgumentNullException(nameof(annonceGateway));
    }

    public void Execute(Annonce annonce)
    {
        ArgumentNullException.ThrowIfNull(annonce);

        if (annonce.Id <= 0)
        {
            throw new ArgumentException("L'identifiant de l'annonce est invalide.", nameof(annonce));
        }

        if (_annonceGateway.GetAnnonceById(annonce.Id) == null)
        {
            throw new InvalidOperationException("L'annonce a modifier est introuvable.");
        }

        if (string.IsNullOrWhiteSpace(annonce.Nom))
        {
            throw new ArgumentException("Le nom de l'annonce est obligatoire.", nameof(annonce));
        }

        if (annonce.Prix < 0)
        {
            throw new ArgumentException("Le prix de l'annonce ne peut pas etre negatif.", nameof(annonce));
        }

        annonce.DerniereModification = DateTime.UtcNow;

        _annonceGateway.UpdateAnnonce(annonce);
    }
}

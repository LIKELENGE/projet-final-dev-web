using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class ValiderAnnonceUseCase : IValiderAnnonceUseCase
{
    private readonly IModererGateway _modererGateway;
    private readonly IAdminGateway _adminGateway;
    private readonly IAnnonceGateway _annonceGateway;
    private readonly IEtatAnnonceGateway _etatAnnonceGateway;

    public ValiderAnnonceUseCase(
        IModererGateway modererGateway,
        IAdminGateway adminGateway,
        IAnnonceGateway annonceGateway,
        IEtatAnnonceGateway etatAnnonceGateway)
    {
        _modererGateway = modererGateway ?? throw new ArgumentNullException(nameof(modererGateway));
        _adminGateway = adminGateway ?? throw new ArgumentNullException(nameof(adminGateway));
        _annonceGateway = annonceGateway ?? throw new ArgumentNullException(nameof(annonceGateway));
        _etatAnnonceGateway = etatAnnonceGateway ?? throw new ArgumentNullException(nameof(etatAnnonceGateway));
    }

    public void Execute(Moderer moderation)
    {
        ArgumentNullException.ThrowIfNull(moderation);

        if (moderation.Admin == null || moderation.Admin.Compte <= 0)
        {
            throw new ArgumentException("La validation doit etre liee a un administrateur.", nameof(moderation));
        }

        if (moderation.Annonce == null || moderation.Annonce.Id <= 0)
        {
            throw new ArgumentException("La validation doit etre liee a une annonce.", nameof(moderation));
        }

        if (moderation.EtatAnnonce == null || moderation.EtatAnnonce.Id <= 0)
        {
            throw new ArgumentException("La validation doit definir un etat d'annonce.", nameof(moderation));
        }

        if (_adminGateway.GetAdminById(moderation.Admin.Compte) == null)
        {
            throw new InvalidOperationException("L'administrateur est introuvable.");
        }

        if (_annonceGateway.GetAnnonceById(moderation.Annonce.Id) == null)
        {
            throw new InvalidOperationException("L'annonce est introuvable.");
        }

        if (_etatAnnonceGateway.GetEtatAnnonceById(moderation.EtatAnnonce.Id) == null)
        {
            throw new InvalidOperationException("L'etat de l'annonce est introuvable.");
        }

        if (moderation.DateStatut == default)
        {
            moderation.DateStatut = DateTime.UtcNow;
        }

        if (moderation.Id <= 0)
        {
            _modererGateway.AddModeration(moderation);
            return;
        }

        _modererGateway.UpdateModeration(moderation);
    }
}

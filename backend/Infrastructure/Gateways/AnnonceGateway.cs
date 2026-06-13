using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class AnnonceGateway : IAnnonceGateway
{
    private readonly IAnnonceRepository _annonceRepository;

    public AnnonceGateway(IAnnonceRepository annonceRepository)
    {
        _annonceRepository = annonceRepository ?? throw new ArgumentNullException(nameof(annonceRepository));
    }

    public IEnumerable<Annonce> GetAllAnnonces()
    {
        return _annonceRepository.GetAllAnnonces();
    }

    public Annonce? GetAnnonceById(int annonceId)
    {
        return _annonceRepository.GetAnnonceById(annonceId);
    }

    public IEnumerable<Annonce> GetAnnoncesByUtilisateurId(int utilisateurId)
    {
        return _annonceRepository.GetAnnoncesByUtilisateurId(utilisateurId);
    }

    public IEnumerable<Annonce> GetAnnoncesByCategorieId(int categorieId)
    {
        return _annonceRepository.GetAnnoncesByCategorieId(categorieId);
    }

    public void AddAnnonce(Annonce annonce)
    {
        _annonceRepository.AddAnnonce(annonce);
    }

    public void UpdateAnnonce(Annonce annonce)
    {
        _annonceRepository.UpdateAnnonce(annonce);
    }

    public void DeleteAnnonce(int annonceId)
    {
        _annonceRepository.DeleteAnnonce(annonceId);
    }
}

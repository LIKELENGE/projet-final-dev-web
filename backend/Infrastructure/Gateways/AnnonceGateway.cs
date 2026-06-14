using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

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
        return _annonceRepository.GetAllAnnonces().Select(annonce => annonce.ToCore());
    }

    public Annonce? GetAnnonceById(int annonceId)
    {
        var annonce = _annonceRepository.GetAnnonceById(annonceId);

        return annonce?.ToCore();
    }

    public IEnumerable<Annonce> GetAnnoncesByUtilisateurId(int utilisateurId)
    {
        return _annonceRepository.GetAnnoncesByUtilisateurId(utilisateurId).Select(annonce => annonce.ToCore());
    }

    public IEnumerable<Annonce> GetAnnoncesByCategorieId(int categorieId)
    {
        return _annonceRepository.GetAnnoncesByCategorieId(categorieId).Select(annonce => annonce.ToCore());
    }

    public void AddAnnonce(Annonce annonce)
    {
        var annonceDb = annonce.ToInfrastructure();
        _annonceRepository.AddAnnonce(annonceDb);
        annonce.Id = annonceDb.IdAnnonce;
    }

    public void UpdateAnnonce(Annonce annonce)
    {
        _annonceRepository.UpdateAnnonce(annonce.ToInfrastructure());
    }

    public void DeleteAnnonce(int annonceId)
    {
        _annonceRepository.DeleteAnnonce(annonceId);
    }
}

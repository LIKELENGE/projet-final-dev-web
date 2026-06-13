using Core.Models;

namespace Core.IGateways;

public interface IAnnonceGateway
{
    IEnumerable<Annonce> GetAllAnnonces();
    Annonce? GetAnnonceById(int annonceId);
    IEnumerable<Annonce> GetAnnoncesByUtilisateurId(int utilisateurId);
    IEnumerable<Annonce> GetAnnoncesByCategorieId(int categorieId);
    void AddAnnonce(Annonce annonce);
    void UpdateAnnonce(Annonce annonce);
    void DeleteAnnonce(int annonceId);
}

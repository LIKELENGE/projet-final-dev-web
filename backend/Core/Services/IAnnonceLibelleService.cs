using Core.Models;

namespace Core.Services;

public interface IAnnonceLibelleService
{
    Annonce Completer(Annonce annonce);
    IEnumerable<Annonce> Completer(IEnumerable<Annonce> annonces);
}

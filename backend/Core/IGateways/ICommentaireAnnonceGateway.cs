using Core.Models;

namespace Core.IGateways;

public interface ICommentaireAnnonceGateway
{
    IEnumerable<CommentaireAnnonce> GetAllCommentairesAnnonce();
    CommentaireAnnonce? GetCommentaireAnnonceById(int commentaireAnnonceId);
    IEnumerable<CommentaireAnnonce> GetCommentairesByAnnonceId(int annonceId);
    void AddCommentaireAnnonce(CommentaireAnnonce commentaireAnnonce);
    void UpdateCommentaireAnnonce(CommentaireAnnonce commentaireAnnonce);
    void DeleteCommentaireAnnonce(int commentaireAnnonceId);
}

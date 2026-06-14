using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Gateways;

public class CommentaireAnnonceGateway : ICommentaireAnnonceGateway
{
    private readonly ICommentaireAnnonceRepository _commentaireAnnonceRepository;

    public CommentaireAnnonceGateway(ICommentaireAnnonceRepository commentaireAnnonceRepository)
    {
        _commentaireAnnonceRepository = commentaireAnnonceRepository ?? throw new ArgumentNullException(nameof(commentaireAnnonceRepository));
    }

    public IEnumerable<CommentaireAnnonce> GetAllCommentairesAnnonce()
    {
        return _commentaireAnnonceRepository.GetAllCommentairesAnnonce().Select(commentaire => commentaire.ToCore());
    }

    public CommentaireAnnonce? GetCommentaireAnnonceById(int commentaireAnnonceId)
    {
        var commentaire = _commentaireAnnonceRepository.GetCommentaireAnnonceById(commentaireAnnonceId);

        return commentaire?.ToCore();
    }

    public IEnumerable<CommentaireAnnonce> GetCommentairesByAnnonceId(int annonceId)
    {
        return _commentaireAnnonceRepository.GetCommentairesByAnnonceId(annonceId).Select(commentaire => commentaire.ToCore());
    }

    public void AddCommentaireAnnonce(CommentaireAnnonce commentaireAnnonce)
    {
        var commentaireDb = commentaireAnnonce.ToInfrastructure();
        _commentaireAnnonceRepository.AddCommentaireAnnonce(commentaireDb);
        commentaireAnnonce.Id = commentaireDb.IdCommentaireAnnonce;
    }

    public void UpdateCommentaireAnnonce(CommentaireAnnonce commentaireAnnonce)
    {
        _commentaireAnnonceRepository.UpdateCommentaireAnnonce(commentaireAnnonce.ToInfrastructure());
    }

    public void DeleteCommentaireAnnonce(int commentaireAnnonceId)
    {
        _commentaireAnnonceRepository.DeleteCommentaireAnnonce(commentaireAnnonceId);
    }
}

using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

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
        return _commentaireAnnonceRepository.GetAllCommentairesAnnonce();
    }

    public CommentaireAnnonce? GetCommentaireAnnonceById(int commentaireAnnonceId)
    {
        return _commentaireAnnonceRepository.GetCommentaireAnnonceById(commentaireAnnonceId);
    }

    public IEnumerable<CommentaireAnnonce> GetCommentairesByAnnonceId(int annonceId)
    {
        return _commentaireAnnonceRepository.GetCommentairesByAnnonceId(annonceId);
    }

    public void AddCommentaireAnnonce(CommentaireAnnonce commentaireAnnonce)
    {
        _commentaireAnnonceRepository.AddCommentaireAnnonce(commentaireAnnonce);
    }

    public void UpdateCommentaireAnnonce(CommentaireAnnonce commentaireAnnonce)
    {
        _commentaireAnnonceRepository.UpdateCommentaireAnnonce(commentaireAnnonce);
    }

    public void DeleteCommentaireAnnonce(int commentaireAnnonceId)
    {
        _commentaireAnnonceRepository.DeleteCommentaireAnnonce(commentaireAnnonceId);
    }
}

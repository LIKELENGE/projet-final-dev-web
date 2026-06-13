using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class CommentairePhotoGateway : ICommentairePhotoGateway
{
    private readonly ICommentairePhotoRepository _commentairePhotoRepository;

    public CommentairePhotoGateway(ICommentairePhotoRepository commentairePhotoRepository)
    {
        _commentairePhotoRepository = commentairePhotoRepository ?? throw new ArgumentNullException(nameof(commentairePhotoRepository));
    }

    public IEnumerable<CommentairePhoto> GetAllCommentairesPhoto()
    {
        return _commentairePhotoRepository.GetAllCommentairesPhoto();
    }

    public CommentairePhoto? GetCommentairePhotoById(int commentairePhotoId)
    {
        return _commentairePhotoRepository.GetCommentairePhotoById(commentairePhotoId);
    }

    public IEnumerable<CommentairePhoto> GetCommentairesByPhotoAnnonceId(int photoAnnonceId)
    {
        return _commentairePhotoRepository.GetCommentairesByPhotoAnnonceId(photoAnnonceId);
    }

    public void AddCommentairePhoto(CommentairePhoto commentairePhoto)
    {
        _commentairePhotoRepository.AddCommentairePhoto(commentairePhoto);
    }

    public void UpdateCommentairePhoto(CommentairePhoto commentairePhoto)
    {
        _commentairePhotoRepository.UpdateCommentairePhoto(commentairePhoto);
    }

    public void DeleteCommentairePhoto(int commentairePhotoId)
    {
        _commentairePhotoRepository.DeleteCommentairePhoto(commentairePhotoId);
    }
}

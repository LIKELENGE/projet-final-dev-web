using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

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
        return _commentairePhotoRepository.GetAllCommentairesPhoto().Select(commentaire => commentaire.ToCore());
    }

    public CommentairePhoto? GetCommentairePhotoById(int commentairePhotoId)
    {
        var commentaire = _commentairePhotoRepository.GetCommentairePhotoById(commentairePhotoId);

        return commentaire?.ToCore();
    }

    public IEnumerable<CommentairePhoto> GetCommentairesByPhotoAnnonceId(int photoAnnonceId)
    {
        return _commentairePhotoRepository.GetCommentairesByPhotoAnnonceId(photoAnnonceId).Select(commentaire => commentaire.ToCore());
    }

    public void AddCommentairePhoto(CommentairePhoto commentairePhoto)
    {
        var commentaireDb = commentairePhoto.ToInfrastructure();
        _commentairePhotoRepository.AddCommentairePhoto(commentaireDb);
        commentairePhoto.Id = commentaireDb.IdCommentairePhoto;
    }

    public void UpdateCommentairePhoto(CommentairePhoto commentairePhoto)
    {
        _commentairePhotoRepository.UpdateCommentairePhoto(commentairePhoto.ToInfrastructure());
    }

    public void DeleteCommentairePhoto(int commentairePhotoId)
    {
        _commentairePhotoRepository.DeleteCommentairePhoto(commentairePhotoId);
    }
}

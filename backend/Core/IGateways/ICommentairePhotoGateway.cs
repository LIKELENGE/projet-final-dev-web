using Core.Models;

namespace Core.IGateways;

public interface ICommentairePhotoGateway
{
    IEnumerable<CommentairePhoto> GetAllCommentairesPhoto();
    CommentairePhoto? GetCommentairePhotoById(int commentairePhotoId);
    IEnumerable<CommentairePhoto> GetCommentairesByPhotoAnnonceId(int photoAnnonceId);
    void AddCommentairePhoto(CommentairePhoto commentairePhoto);
    void UpdateCommentairePhoto(CommentairePhoto commentairePhoto);
    void DeleteCommentairePhoto(int commentairePhotoId);
}

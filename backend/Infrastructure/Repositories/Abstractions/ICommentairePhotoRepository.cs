using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface ICommentairePhotoRepository
    {
        IEnumerable<CommentairePhoto> GetAllCommentairesPhoto();
        CommentairePhoto? GetCommentairePhotoById(int commentairePhotoId);
        IEnumerable<CommentairePhoto> GetCommentairesByPhotoAnnonceId(int photoAnnonceId);
        void AddCommentairePhoto(CommentairePhoto commentairePhoto);
        void UpdateCommentairePhoto(CommentairePhoto commentairePhoto);
        void DeleteCommentairePhoto(int commentairePhotoId);
    }
}
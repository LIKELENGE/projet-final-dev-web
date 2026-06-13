using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface ICommentaireAnnonceRepository
    {
        IEnumerable<CommentaireAnnonce> GetAllCommentairesAnnonce();
        CommentaireAnnonce? GetCommentaireAnnonceById(int commentaireAnnonceId);
        IEnumerable<CommentaireAnnonce> GetCommentairesByAnnonceId(int annonceId);
        void AddCommentaireAnnonce(CommentaireAnnonce commentaireAnnonce);
        void UpdateCommentaireAnnonce(CommentaireAnnonce commentaireAnnonce);
        void DeleteCommentaireAnnonce(int commentaireAnnonceId);
    }
}
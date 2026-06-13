using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IPhotoAnnonceRepository
    {
        IEnumerable<PhotoAnnonce> GetAllPhotosAnnonce();
        PhotoAnnonce? GetPhotoAnnonceById(int photoAnnonceId);
        IEnumerable<PhotoAnnonce> GetPhotosByAnnonceId(int annonceId);
        void AddPhotoAnnonce(PhotoAnnonce photoAnnonce);
        void UpdatePhotoAnnonce(PhotoAnnonce photoAnnonce);
        void DeletePhotoAnnonce(int photoAnnonceId);
    }
}
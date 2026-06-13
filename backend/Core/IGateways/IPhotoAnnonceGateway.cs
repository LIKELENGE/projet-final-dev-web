using Core.Models;

namespace Core.IGateways;

public interface IPhotoAnnonceGateway
{
    IEnumerable<PhotoAnnonce> GetAllPhotosAnnonce();
    PhotoAnnonce? GetPhotoAnnonceById(int photoAnnonceId);
    IEnumerable<PhotoAnnonce> GetPhotosByAnnonceId(int annonceId);
    void AddPhotoAnnonce(PhotoAnnonce photoAnnonce);
    void UpdatePhotoAnnonce(PhotoAnnonce photoAnnonce);
    void DeletePhotoAnnonce(int photoAnnonceId);
}

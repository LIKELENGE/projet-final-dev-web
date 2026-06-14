using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Gateways;

public class PhotoAnnonceGateway : IPhotoAnnonceGateway
{
    private readonly IPhotoAnnonceRepository _photoAnnonceRepository;

    public PhotoAnnonceGateway(IPhotoAnnonceRepository photoAnnonceRepository)
    {
        _photoAnnonceRepository = photoAnnonceRepository ?? throw new ArgumentNullException(nameof(photoAnnonceRepository));
    }

    public IEnumerable<PhotoAnnonce> GetAllPhotosAnnonce()
    {
        return _photoAnnonceRepository.GetAllPhotosAnnonce().Select(photo => photo.ToCore());
    }

    public PhotoAnnonce? GetPhotoAnnonceById(int photoAnnonceId)
    {
        var photo = _photoAnnonceRepository.GetPhotoAnnonceById(photoAnnonceId);

        return photo?.ToCore();
    }

    public IEnumerable<PhotoAnnonce> GetPhotosByAnnonceId(int annonceId)
    {
        return _photoAnnonceRepository.GetPhotosByAnnonceId(annonceId).Select(photo => photo.ToCore());
    }

    public void AddPhotoAnnonce(PhotoAnnonce photoAnnonce)
    {
        var photoDb = photoAnnonce.ToInfrastructure();
        _photoAnnonceRepository.AddPhotoAnnonce(photoDb);
        photoAnnonce.Id = photoDb.IdPhotoAnnonce;
    }

    public void UpdatePhotoAnnonce(PhotoAnnonce photoAnnonce)
    {
        _photoAnnonceRepository.UpdatePhotoAnnonce(photoAnnonce.ToInfrastructure());
    }

    public void DeletePhotoAnnonce(int photoAnnonceId)
    {
        _photoAnnonceRepository.DeletePhotoAnnonce(photoAnnonceId);
    }
}

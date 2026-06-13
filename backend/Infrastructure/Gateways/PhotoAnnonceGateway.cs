using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

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
        return _photoAnnonceRepository.GetAllPhotosAnnonce();
    }

    public PhotoAnnonce? GetPhotoAnnonceById(int photoAnnonceId)
    {
        return _photoAnnonceRepository.GetPhotoAnnonceById(photoAnnonceId);
    }

    public IEnumerable<PhotoAnnonce> GetPhotosByAnnonceId(int annonceId)
    {
        return _photoAnnonceRepository.GetPhotosByAnnonceId(annonceId);
    }

    public void AddPhotoAnnonce(PhotoAnnonce photoAnnonce)
    {
        _photoAnnonceRepository.AddPhotoAnnonce(photoAnnonce);
    }

    public void UpdatePhotoAnnonce(PhotoAnnonce photoAnnonce)
    {
        _photoAnnonceRepository.UpdatePhotoAnnonce(photoAnnonce);
    }

    public void DeletePhotoAnnonce(int photoAnnonceId)
    {
        _photoAnnonceRepository.DeletePhotoAnnonce(photoAnnonceId);
    }
}

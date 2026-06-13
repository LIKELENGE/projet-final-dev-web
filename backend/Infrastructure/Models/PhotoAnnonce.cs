using System;

namespace Infrastructure.Models
{
    public class PhotoAnnonce
    {
        public int IdPhotoAnnonce { get; set; }

        public int IdAnnonce { get; set; }

        public string? Titre { get; set; }

        public string Lien { get; set; } = string.Empty;
    }
}
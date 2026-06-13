using System;

namespace Infrastructure.Models
{
    public class Commune
    {
        public int IdCommune { get; set; }

        public string NomCommune { get; set; } = string.Empty;

        public string? CodePostal { get; set; }
    }
}
using System;

namespace Infrastructure.Models
{
    public class Dimension
    {
        public int IdDimension { get; set; }

        public int IdAnnonce { get; set; }

        public double? ProfondeurCm { get; set; }

        public double? LongueurCm { get; set; }

        public double? LargeurCm { get; set; }

        public double? PoidsKg { get; set; }
    }
}
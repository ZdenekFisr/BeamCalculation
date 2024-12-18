using Application.Enums;

namespace Application.Input
{
    public class Beam
    {
        public double Length { get; set; }

        public double OverlapA { get; set; }

        public double OverlapB { get; set; }

        public double TotalLength => Length + OverlapA + OverlapB;

        public BeamType Type { get; set; }

        public required ICollection<Load> Loads { get; set; }

        public required ICollection<Modulus> Moduli { get; set; }
    }
}

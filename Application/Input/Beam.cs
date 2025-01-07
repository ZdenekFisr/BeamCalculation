using Application.Enums;

namespace Application.Input
{
    /// <summary>
    /// Represents a beam with loads and section moduli.
    /// </summary>
    public class Beam
    {
        /// <summary>
        /// The length of the beam between supports or from one support to end in meters.
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// The length of the overlap on the left side of the beam in meters.
        /// </summary>
        public double OverlapA { get; set; }

        /// <summary>
        /// The length of the overlap on the right side of the beam in meters.
        /// </summary>
        public double OverlapB { get; set; }

        /// <summary>
        /// The total length of the beam including overlaps in meters.
        /// </summary>
        public double TotalLength => Length + OverlapA + OverlapB;

        /// <summary>
        /// The type of the beam.
        /// </summary>
        public BeamType Type { get; set; }

        /// <summary>
        /// Collection of loads on the beam.
        /// </summary>
        public required ICollection<Load> Loads { get; set; }

        /// <summary>
        /// Collection of section moduli of the beam.
        /// </summary>
        public required ICollection<Modulus> Moduli { get; set; }
    }
}

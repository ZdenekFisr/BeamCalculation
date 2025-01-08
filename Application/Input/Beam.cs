namespace Application.Input
{
    /// <summary>
    /// Represents a beam with loads and section moduli.
    /// </summary>
    public abstract class Beam
    {
        /// <summary>
        /// The total length of the beam including overlaps in meters.
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// The position of support A from the left edge of the beam in meters.
        /// </summary>
        public virtual double SupportA { get; set; }

        /// <summary>
        /// Collection of loads on the beam.
        /// </summary>
        public required ICollection<Load> Loads { get; set; }

        /// <summary>
        /// Collection of section moduli of the beam.
        /// </summary>
        public required ICollection<Modulus> Moduli { get; set; }

        /// <summary>
        /// Returns the reactions transferred through the supports.
        /// </summary>
        /// <returns>Collection of reactions represented as loads.</returns>
        public abstract ICollection<Load> GetReactions();
    }
}

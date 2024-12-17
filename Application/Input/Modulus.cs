namespace Application.Input
{
    /// <summary>
    /// Represents the section modulus in bending.
    /// </summary>
    public class Modulus
    {
        /// <summary>
        /// The point where the section starts in meters from the left edge.
        /// </summary>
        public double From { get; set; }

        /// <summary>
        /// Value of the section modulus in m^3.
        /// </summary>
        public double Value { get; set; }
    }
}

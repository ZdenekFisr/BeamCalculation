namespace Application.PointComponents
{
    /// <summary>
    /// Represents a shear force on a point of the beam.
    /// </summary>
    public interface IForce
    {
        /// <summary>
        /// The position of the point in meters from the left edge of the beam.
        /// </summary>
        public double Position { get; set; }

        /// <summary>
        /// The value of the force in N.
        /// </summary>
        public double Force { get; set; }

        /// <summary>
        /// The value of the force in N. This has value when the force changes its value suddenly; otherwise, it is null.
        /// </summary>
        public double? ForceJump { get; set; }
    }
}

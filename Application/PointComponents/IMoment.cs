namespace Application.PointComponents
{
    /// <summary>
    /// Represents a bending moment on a point of the beam.
    /// </summary>
    public interface IMoment
    {
        /// <summary>
        /// The position of the point in meters from the left edge of the beam.
        /// </summary>
        public double Position { get; set; }

        /// <summary>
        /// The value of the moment in Nm.
        /// </summary>
        public double Moment { get; set; }

        /// <summary>
        /// The value of the moment in Nm. This has value when the moment changes its value suddenly; otherwise, it is null.
        /// </summary>
        public double? MomentJump { get; set; }
    }
}

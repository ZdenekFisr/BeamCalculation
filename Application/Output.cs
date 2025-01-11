using Application.Input;

namespace Application
{
    /// <summary>
    /// Represents the output of the application, containing points and reactions.
    /// </summary>
    public class Output
    {
        /// <summary>
        /// Gets or sets the points on the beam.
        /// </summary>
        public required Point[] Points { get; set; }

        /// <summary>
        /// Gets or sets the first reaction load.
        /// </summary>
        public required Load Reaction1 { get; set; }

        /// <summary>
        /// Gets or sets the second reaction load.
        /// </summary>
        public required Load Reaction2 { get; set; }

        /// <summary>
        /// Gets the maximum force among all points.
        /// </summary>
        public double MaxForce => Points.Max(p => Math.Abs(p.Force));

        /// <summary>
        /// Gets the maximum moment among all points.
        /// </summary>
        public double MaxMoment => Points.Max(p => Math.Abs(p.Moment));

        /// <summary>
        /// Gets the maximum stress among all points.
        /// </summary>
        public double MaxStress => Points.Max(p => Math.Abs(p.Stress));
    }
}

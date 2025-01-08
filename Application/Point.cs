using Application.PointComponents;

namespace Application
{
    /// <summary>
    /// Represents a point on the beam. Each point has a position, shear force, bending moment, modulus, and mechanical stress.
    /// </summary>
    public class Point : IForce, IMoment
    {
        public double Position { get; set; }
        public double Force { get; set; }
        public double? ForceJump { get; set; }
        public double Moment { get; set; }
        public double? MomentJump { get; set; }
        public double Modulus { get; set; }
        public double Stress { get; set; }
        public double? StressJump { get; set; }
    }
}

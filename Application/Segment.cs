using Application.SegmentComponents;

namespace Application
{
    public class Segment : IForce
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

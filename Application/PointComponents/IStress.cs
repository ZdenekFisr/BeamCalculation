namespace Application.PointComponents
{
    public interface IStress : IMoment
    {
        /// <summary>
        /// The value of the mechanical stress in bending in N/mm^2 (MPa).
        /// </summary>
        public double Stress { get; set; }

        /// <summary>
        /// The value of the mechanical stress in bending in N/mm^2 (MPa). This has value when the stress changes its value suddenly; otherwise, it is null.
        /// </summary>
        public double? StressJump { get; set; }
    }
}

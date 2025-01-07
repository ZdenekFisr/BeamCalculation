namespace Application.Input
{
    /// <summary>
    /// Represents a moment load on the beam.
    /// </summary>
    public class MomentLoad : Load
    {
        /// <summary>
        /// The value of the moment in Nm.
        /// </summary>
        public override double Value { get; set; }

        public override double GetInfluenceOnShearForce(double position)
        {
            return 0;
        }

        public override double GetInfluenceOnBendingMoment(double position)
        {
            throw new NotImplementedException();
        }

        public override ICollection<Load> GetSimplifiedLoads()
        {
            return [this];
        }
    }
}

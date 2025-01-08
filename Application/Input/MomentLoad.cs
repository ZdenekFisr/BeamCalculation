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

        public override double GetInfluenceOnShearForce(double currentPointPosition)
        {
            return 0;
        }

        public override double GetInfluenceOnBendingMoment(double currentPointPosition)
        {
            if (Position <= currentPointPosition)
                return -Value;

            return 0;
        }

        public override ICollection<Load> GetSimplifiedLoads()
        {
            return [this];
        }
    }
}

namespace Application.Input
{
    /// <summary>
    /// Represents a force load on the beam.
    /// </summary>
    public class ForceLoad : Load
    {
        /// <summary>
        /// The value of the force in N.
        /// </summary>
        public override double Value { get; set; }

        public override double GetInfluenceOnShearForce(double currentPointPosition)
        {
            if (Position <= currentPointPosition)
                return Value;

            return 0;
        }

        public override double GetInfluenceOnBendingMoment(double currentPointPosition)
        {
            if (Position < currentPointPosition)
                return Value * (currentPointPosition - Position);

            return 0;
        }

        public override ICollection<Load> GetSimplifiedLoads()
        {
            return [this];
        }
    }
}

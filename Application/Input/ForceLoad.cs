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

        public override (double, double?) GetInfluenceOnShearForce(double currentPointPosition)
        {
            double force = Position <= currentPointPosition ? Value : 0;
            double? forceBeforeJump = Position == currentPointPosition ? Value : null;
            return (force, forceBeforeJump);
        }

        public override (double, double?) GetInfluenceOnBendingMoment(double currentPointPosition)
        {
            double moment = Position < currentPointPosition ? Value * (currentPointPosition - Position) : 0;
            return (moment, null);
        }

        public override ICollection<Load> GetSimplifiedLoads()
        {
            return [this];
        }
    }
}

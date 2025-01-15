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

        public override (double, double?) GetInfluenceOnShearForce(double currentPointPosition)
        {
            return (0, null);
        }

        public override (double, double?) GetInfluenceOnBendingMoment(double currentPointPosition)
        {
            double moment = Position <= currentPointPosition ? -Value : 0;
            double? momentBeforeJump = Position == currentPointPosition ? -Value : null;
            return (moment, momentBeforeJump);
        }

        public override ICollection<Load> GetSimplifiedLoads()
        {
            return [this];
        }
    }
}

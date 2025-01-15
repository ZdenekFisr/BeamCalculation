namespace Application.Input
{
    /// <summary>
    /// Represents a general load on the beam.
    /// </summary>
    public abstract class Load
    {
        /// <summary>
        /// General value of the load.
        /// </summary>
        public virtual double Value { get; set; }

        /// <summary>
        /// The position of the load from the left edge of the beam in meters.
        /// </summary>
        public double Position { get; set; }

        /// <summary>
        /// Returns the influence of the load on the shear force at a given position.
        /// </summary>
        /// <param name="currentPointPosition">Position on the beam in meters.</param>
        /// <returns>Influence on the shear force in N.</returns>
        public abstract (double force, double? forceBeforeJump) GetInfluenceOnShearForce(double currentPointPosition);

        /// <summary>
        /// Returns the influence of the load on the bending moment at a given position.
        /// </summary>
        /// <param name="currentPointPosition">Position on the beam in meters.</param>
        /// <returns>Influence on the bending moment in Nm.</returns>
        public abstract (double moment, double? momentBeforeJump) GetInfluenceOnBendingMoment(double currentPointPosition);

        /// <summary>
        /// Returns a simplified version of the load.
        /// </summary>
        /// <returns>A collection of loads that represent this load in simplified terms.</returns>
        public abstract ICollection<Load> GetSimplifiedLoads();
    }
}

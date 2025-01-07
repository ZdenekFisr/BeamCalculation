namespace Application.Input
{
    /// <summary>
    /// Represents a continuous load on the beam.
    /// </summary>
    public class ContinuousLoad : Load
    {
        /// <summary>
        /// The value of the load in N/m.
        /// </summary>
        public override double Value { get; set; }

        /// <summary>
        /// The length of the load in meters.
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// The coefficient the value of the load is multiplied by at the start position.
        /// </summary>
        public double StartCoefficient { get; set; }

        /// <summary>
        /// The coefficient the value of the load is multiplied by at the end position.
        /// </summary>
        public double EndCoefficient { get; set; }

        public override double GetInfluenceOnShearForce(double position)
        {
            if (position <= Position)
                return 0;

            if (Position + Length <= position)
                return Value * (EndCoefficient + StartCoefficient) / 2;

            double coefficient = StartCoefficient + (EndCoefficient - StartCoefficient) * (position - Position) / Length;
            return Value * (StartCoefficient + coefficient) / 2 * (position - Position) / Length;
        }

        public override double GetInfluenceOnBendingMoment(double position)
        {
            throw new NotImplementedException();
        }

        public override ICollection<Load> GetSimplifiedLoads()
        {
            List<Load> result = [];

            if (StartCoefficient != 0 && EndCoefficient != 0)
            {
                result.Add(new ForceLoad
                {
                    Value = Value * (EndCoefficient + StartCoefficient) / 2 * Length,
                    Position = Position + Length / 2
                });
            }

            if (StartCoefficient == EndCoefficient)
                return result;

            if (StartCoefficient < EndCoefficient)
            {
                result.Add(new ForceLoad
                {
                    Value = Value * (EndCoefficient - StartCoefficient) * Length / 2,
                    Position = Position + 2 * Length / 3
                });
                return result;
            }

            result.Add(new ForceLoad
            {
                Value = Value * (StartCoefficient - EndCoefficient) * Length / 2,
                Position = Position + Length / 3
            });
            return result;
        }
    }
}

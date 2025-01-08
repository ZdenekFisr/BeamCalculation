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

        public override double GetInfluenceOnShearForce(double currentPointPosition)
        {
            if (currentPointPosition <= Position)
                return 0;

            if (currentPointPosition <= Position + Length)
                return Value * (EndCoefficient + StartCoefficient) / 2;

            return GetForceAtPosition(currentPointPosition);
        }

        public override double GetInfluenceOnBendingMoment(double currentPointPosition)
        {
            if (currentPointPosition <= Position)
                return 0;

            if (currentPointPosition <= Position + Length)
            {
                double coefficient = GetCurrentCoefficient(currentPointPosition);

                Load partialLoad = new ContinuousLoad
                {
                    Value = Value,
                    Position = Position,
                    Length = currentPointPosition - Position,
                    StartCoefficient = StartCoefficient,
                    EndCoefficient = coefficient
                };

                Load[] partialLoadsSimplified = [.. partialLoad.GetSimplifiedLoads()];
                return partialLoadsSimplified.Sum(load => load.Value * load.Position);
            }

            Load[] loadsSimplified = [.. GetSimplifiedLoads()];
            return loadsSimplified.Sum(load => load.Value * load.Position);
        }

        private double GetCurrentCoefficient(double position)
        {
            return StartCoefficient + (EndCoefficient - StartCoefficient) * (position - Position) / Length;
        }

        private double GetForceAtPosition(double position)
        {
            double coefficient = GetCurrentCoefficient(position);
            return Value * (StartCoefficient + coefficient) / 2 * (position - Position) / Length;
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

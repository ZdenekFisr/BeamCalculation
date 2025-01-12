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
                return partialLoadsSimplified.Sum(load => load.Value);
            }

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
            if (StartCoefficient == 0 && EndCoefficient == 0)
                return [];

            if (StartCoefficient == 0)
                return [new ForceLoad { Position = Position + 2 * Length / 3, Value = Value * Length / 2 }];

            if (EndCoefficient == 0)
                return [new ForceLoad { Position = Position + Length / 3, Value = Value * Length / 2 }];

            List<Load> result = [];

            if (Math.Sign(StartCoefficient) != Math.Sign(EndCoefficient))
            {
                double totalRange = Math.Abs(StartCoefficient) + Math.Abs(EndCoefficient);

                if (StartCoefficient < 0)
                {
                    double negativePartLength = Length * Math.Abs(StartCoefficient) / totalRange;
                    double positivePartLength = Length * Math.Abs(EndCoefficient) / totalRange;

                    result.Add(new ForceLoad
                    {
                        Value = Value * StartCoefficient / 2 * negativePartLength,
                        Position = Position + negativePartLength / 3
                    });
                    result.Add(new ForceLoad
                    {
                        Value = Value * EndCoefficient / 2 * positivePartLength,
                        Position = Position + Length - positivePartLength / 3
                    });
                }
                else
                {
                    double positivePartLength = Length * Math.Abs(StartCoefficient) / totalRange;
                    double negativePartLength = Length * Math.Abs(EndCoefficient) / totalRange;

                    result.Add(new ForceLoad
                    {
                        Value = Value * StartCoefficient / 2 * positivePartLength,
                        Position = Position + positivePartLength / 3
                    });
                    result.Add(new ForceLoad
                    {
                        Value = Value * EndCoefficient / 2 * negativePartLength,
                        Position = Position + Length - negativePartLength / 3
                    });
                }

                return result;
            }

            if (StartCoefficient < 0 && EndCoefficient < 0)
            {
                Value = -Value;
                StartCoefficient = -StartCoefficient;
                EndCoefficient = -EndCoefficient;
            }

            result.Add(new ForceLoad
            {
                Value = Value * Math.Min(StartCoefficient, EndCoefficient) * Length,
                Position = Position + Length / 2
            });

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

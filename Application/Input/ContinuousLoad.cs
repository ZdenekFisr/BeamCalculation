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

        public override (double, double?) GetInfluenceOnShearForce(double currentPointPosition)
        {
            double force;
            if (currentPointPosition <= Position)
                force = 0;
            else if (currentPointPosition <= Position + Length)
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
                force = partialLoadsSimplified.Sum(load => load.Value);
            }
            else
                force = Value * Length;

            return (force, null);
        }

        public override (double, double?) GetInfluenceOnBendingMoment(double currentPointPosition)
        {
            double moment;
            if (currentPointPosition <= Position)
                moment = 0;

            else if (currentPointPosition <= Position + Length)
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
                moment = partialLoadsSimplified.Sum(load => load.Value * (currentPointPosition - load.Position));
            }

            else
            {
                Load[] loadsSimplified = [.. GetSimplifiedLoads()];
                moment = loadsSimplified.Sum(load => load.Value * (currentPointPosition - load.Position));
            }

            return (moment, null);
        }

        private double GetCurrentCoefficient(double position)
        {
            return StartCoefficient + (EndCoefficient - StartCoefficient) * (position - Position) / Length;
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

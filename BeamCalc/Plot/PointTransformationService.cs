using Application;

namespace BeamCalc.Plot
{
    /// <inheritdoc cref="IPointTransformationService">
    public class PointTransformationService : IPointTransformationService
    {
        private const int floatingPointRound = 10;

        /// <inheritdoc />
        public PointForPlot[] TransformPoints(Point[] points, Func<Point, double> basicProperty, Func<Point, double?> jumpProperty)
        {
            List<PointForPlot> pointsForPlot = [];

            foreach (Point point in points)
            {
                double x = Math.Round(point.Position, floatingPointRound);
                if (jumpProperty(point).HasValue)
                {
                    pointsForPlot.Add(new PointForPlot
                    {
                        X = x,
                        Y = Math.Round(jumpProperty(point)!.Value, floatingPointRound)
                    });
                }
                pointsForPlot.Add(
                    new PointForPlot
                    {
                        X = x,
                        Y = Math.Round(basicProperty(point), floatingPointRound)
                    });
            }

            return [.. pointsForPlot];
        }
    }
}

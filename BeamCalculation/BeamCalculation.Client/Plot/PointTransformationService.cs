using Application;

namespace BeamCalculation.Client.Plot
{
    /// <inheritdoc cref="IPointTransformationService">
    public class PointTransformationService : IPointTransformationService
    {
        /// <inheritdoc />
        public PointForPlot[] TransformPoints(Point[] points, Func<Point, double> basicProperty, Func<Point, double?> jumpProperty)
        {
            List<PointForPlot> pointsForPlot = [];

            foreach (Point point in points)
            {
                if (jumpProperty(point).HasValue)
                {
                    pointsForPlot.Add(new PointForPlot { X = point.Position, Y = jumpProperty(point)!.Value });
                }
                pointsForPlot.Add(new PointForPlot { X = point.Position, Y = basicProperty(point) });
            }

            return [.. pointsForPlot];
        }
    }
}

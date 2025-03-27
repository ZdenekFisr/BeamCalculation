using Application;

namespace BeamCalc.Plot
{
    /// <summary>
    /// Service for transforming points for plotting.
    /// </summary>
    public interface IPointTransformationService
    {
        /// <summary>
        /// Transforms an array of points into an array of points for plotting.
        /// </summary>
        /// <param name="points">The array of points to transform.</param>
        /// <param name="basicProperty">A function to extract a basic property from a point.</param>
        /// <param name="jumpProperty">A function to extract a jump property from a point.</param>
        /// <returns>An array of transformed points for plotting.</returns>
        PointForPlot[] TransformPoints(Point[] points, Func<Point, double> basicProperty, Func<Point, double?> jumpProperty);
    }
}
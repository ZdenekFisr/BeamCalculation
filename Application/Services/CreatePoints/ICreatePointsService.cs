namespace Application.Services.CreatePoints
{
    /// <summary>
    /// Service for creating points on the beam.
    /// </summary>
    public interface ICreatePointsService
    {
        /// <summary>
        /// Creates points on the beam for which the mechanical values will be calculated.
        /// </summary>
        /// <param name="beamLength">Total beam length in meters.</param>
        /// <param name="lengthBetweenPoints">Accuracy (length between points in meters).</param>
        /// <returns>Points with a count increased by one to include both start and end points.</returns>
        Point[] CreatePoints(double beamLength, double lengthBetweenPoints);
    }
}
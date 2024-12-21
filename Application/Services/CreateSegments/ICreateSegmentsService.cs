namespace Application.Services.CreateSegments
{
    /// <summary>
    /// Service for creating segments.
    /// </summary>
    public interface ICreateSegmentsService
    {
        /// <summary>
        /// Splits the beam into segments.
        /// </summary>
        /// <param name="beamLength">Total beam length in meters.</param>
        /// <param name="segmentLength">Accuracy (segment length in meters).</param>
        /// <returns>Segments with a count increased by one to include both start and end points.</returns>
        Segment[] CreateSegments(double beamLength, double segmentLength);
    }
}
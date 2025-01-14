using Application.Exceptions;

namespace Application.Services.CreatePoints
{
    /// <inheritdoc cref="ICreatePointsService"/>
    public class CreatePointsService : ICreatePointsService
    {
        /// <inheritdoc />
        public Point[] CreatePoints(double beamLength, double lengthBetweenPoints)
        {
            if (beamLength <= 0)
                throw new InvalidBeamLengthException();

            if (lengthBetweenPoints <= 0)
                throw new InvalidLengthBetweenPointsException();

            int pointsCount = (int)Math.Ceiling(Math.Round(beamLength / lengthBetweenPoints, 10)) + 1;

            Point[] points = new Point[pointsCount];
            for (int i = 0; i < pointsCount; i++)
            {
                points[i] = new Point
                {
                    Position = i * lengthBetweenPoints
                };
            }

            return points;
        }
    }
}

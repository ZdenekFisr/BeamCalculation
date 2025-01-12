using Application.Services.CreatePoints;
using FluentAssertions;

namespace Application.UnitTests
{
    public class CreatePointsServiceTests
    {
        private readonly CreatePointsService _createPointsService;

        public CreatePointsServiceTests()
        {
            _createPointsService = new();
        }

        private void PerformTest(double beamLength, double lengthBetweenPoints, int expectedPointsCount)
        {
            Point[] points = _createPointsService.CreatePoints(beamLength, lengthBetweenPoints);
            points.Length.Should().Be(expectedPointsCount);
        }

        [Fact]
        public void CreatePoints_SpacesFit()
            => PerformTest(1, 0.001, 1001);

        [Fact]
        public void CreatePoints_SpacesDoNotFit()
            => PerformTest(10.01, 0.02, 502);
    }
}

using Application.Exceptions;
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

        private void PerformTest<T>(double beamLength, double lengthBetweenPoints)
            where T : Exception
        {
            _createPointsService.Invoking(x => x.CreatePoints(beamLength, lengthBetweenPoints)).Should().Throw<T>();
        }

        [Fact]
        public void CreatePoints_SpacesFit()
            => PerformTest(1, 0.001, 1001);

        [Fact]
        public void CreatePoints_SpacesDoNotFit()
            => PerformTest(10.01, 0.02, 502);

        [Fact]
        public void CreatePoints_ZeroBeamLength()
            => PerformTest<InvalidBeamLengthException>(0, 0.001);

        [Fact]
        public void CreatePoints_NegativeBeamLength()
            => PerformTest<InvalidBeamLengthException>(-1, 0.001);

        [Fact]
        public void CreatePoints_ZeroLengthBetweenPoints()
            => PerformTest<InvalidLengthBetweenPointsException>(1, 0);

        [Fact]
        public void CreatePoints_NegativeLengthBetweenPoints()
            => PerformTest<InvalidLengthBetweenPointsException>(1, -0.001);
    }
}

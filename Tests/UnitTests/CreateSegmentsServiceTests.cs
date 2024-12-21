using Application;
using Application.Services.CreateSegments;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class CreateSegmentsServiceTests
    {
        private readonly CreateSegmentsService _createSegmentsService;

        public CreateSegmentsServiceTests()
        {
            _createSegmentsService = new();
        }

        private void PerformTest(double beamLength, double segmentLength, int expectedSegmentsCount)
        {
            Segment[] segments = _createSegmentsService.CreateSegments(beamLength, segmentLength);
            segments.Length.Should().Be(expectedSegmentsCount);
        }

        [Fact]
        public void CreateSegments_NoResidualSegment()
            => PerformTest(1, 0.001, 1001);

        [Fact]
        public void CreateSegments_ResidualSegment()
            => PerformTest(10.01, 0.02, 502);
    }
}

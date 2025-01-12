using Application.Input;
using Application.PointComponents;
using Application.Services.AssignForce;
using FluentAssertions;

namespace Application.IntegrationTests
{
    public class AssignForceServiceTests
    {
        private readonly AssignForceService _assignForceService;

        public AssignForceServiceTests()
        {
            _assignForceService = new();
        }

        private IForce[] ArrangePoints(int pointCount)
        {
            IForce[] points = new IForce[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                points[i] = new TestPoint
                {
                    Position = (double)i / 1000
                };
            }
            return points;
        }

        [Fact]
        public void AssignForce_TwoForcesAtEdges()
        {
            IForce[] points = ArrangePoints(1001);

            Load[] loads =
            [
                new ForceLoad { Position = 0, Value = 1000 },
                new ForceLoad { Position = 1, Value = -1000 }
            ];

            _assignForceService.AssignForce(points, loads);

            points[0].ForceJump.Should().Be(0);
            points[0].Force.Should().Be(1000);
            points[500].ForceJump.Should().BeNull();
            points[500].Force.Should().Be(1000);
            points[1000].ForceJump.Should().Be(1000);
            points[1000].Force.Should().Be(0);
        }

        [Fact]
        public void AssignForce_TwoSupportsOneForce()
        {
            IForce[] points = ArrangePoints(1001);

            Load[] loads =
            [
                new ForceLoad { Position = 0.1, Value = 1000 },
                new ForceLoad { Position = 0.5, Value = -2000 },
                new ForceLoad { Position = 0.9, Value = 1000 }
            ];

            _assignForceService.AssignForce(points, loads);

            points[0].ForceJump.Should().BeNull();
            points[0].Force.Should().Be(0);
            points[100].ForceJump.Should().Be(0);
            points[100].Force.Should().Be(1000);
            points[500].ForceJump.Should().Be(1000);
            points[500].Force.Should().Be(-1000);
            points[900].ForceJump.Should().Be(-1000);
            points[900].Force.Should().Be(0);
            points[1000].ForceJump.Should().BeNull();
            points[1000].Force.Should().Be(0);
        }

        [Fact]
        public void AssignForce_OneSupportOneContinuousLoad()
        {
            IForce[] points = ArrangePoints(1001);

            Load[] loads =
            [
                new MomentLoad { Position = 0, Value = 500 },
                new ForceLoad { Position = 0, Value = 1000 },
                new ContinuousLoad { Position = 0, Length = 1, Value = -1000, StartCoefficient = 1, EndCoefficient = 1 }
            ];

            _assignForceService.AssignForce(points, loads);

            points[0].ForceJump.Should().Be(0);
            points[0].Force.Should().Be(1000);
            points[250].ForceJump.Should().BeNull();
            points[250].Force.Should().Be(750);
            points[500].ForceJump.Should().BeNull();
            points[500].Force.Should().Be(500);
            points[750].ForceJump.Should().BeNull();
            points[750].Force.Should().Be(250);
            points[1000].ForceJump.Should().BeNull();
            points[1000].Force.Should().Be(0);
        }

        [Fact]
        public void AssignForce_TwoSupportsOneContinuousLoad()
        {
            IForce[] points = ArrangePoints(1001);

            Load[] loads =
            [
                new ForceLoad { Position = 0, Value = 1000 },
                new ContinuousLoad { Position = 0, Length = 1, Value = -2000, StartCoefficient = 1, EndCoefficient = 1 },
                new ForceLoad { Position = 1, Value = 1000 }
            ];

            _assignForceService.AssignForce(points, loads);

            points[0].ForceJump.Should().Be(0);
            points[0].Force.Should().Be(1000);
            points[250].ForceJump.Should().BeNull();
            points[250].Force.Should().Be(500);
            points[500].ForceJump.Should().BeNull();
            points[500].Force.Should().Be(0);
            points[750].ForceJump.Should().BeNull();
            points[750].Force.Should().Be(-500);
            points[1000].ForceJump.Should().Be(-1000);
            points[1000].Force.Should().Be(0);
        }

        [Fact]
        public void AssignForce_OneSupportOneRisingContinousLoad()
        {
            IForce[] points = ArrangePoints(1201);

            Load[] loads =
            [
                new ForceLoad { Position = 0, Value = 900 },
                new MomentLoad { Position = 0, Value = 840 },
                new ContinuousLoad { Position = 0, Length = 1.2, Value = -1000, StartCoefficient = 0.5, EndCoefficient = 1 }
            ];

            _assignForceService.AssignForce(points, loads);

            points[0].ForceJump.Should().Be(0);
            points[0].Force.Should().Be(900);
            points[1200].ForceJump.Should().BeNull();
            points[1200].Force.Should().Be(0);
        }
    }

    file class TestPoint : IForce
    {
        public double Position { get; set; }
        public double Force { get; set; }
        public double? ForceJump { get; set; }
    }
}

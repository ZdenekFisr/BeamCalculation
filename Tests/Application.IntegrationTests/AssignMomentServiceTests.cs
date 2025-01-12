using Application.Input;
using Application.PointComponents;
using Application.Services.AssignMoment;
using FluentAssertions;

namespace Application.IntegrationTests
{
    public class AssignMomentServiceTests
    {
        private readonly AssignMomentService _assignMomentService;
        private const double epsilon = 1e-6;

        public AssignMomentServiceTests()
        {
            _assignMomentService = new();
        }

        private IMoment[] ArrangePoints(int pointCount)
        {
            IMoment[] points = new IMoment[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                points[i] = new TestMoment
                {
                    Position = (double)i / 1000
                };
            }
            return points;
        }

        [Fact]
        public void AssignMoment_OneSupportOneForceAtEnd()
        {
            IMoment[] points = ArrangePoints(1001);

            Load[] loads =
            [
                new ForceLoad { Position = 0, Value = 1000 },
                new MomentLoad { Position = 0, Value = 1000 },
                new ForceLoad { Position = 1, Value = -1000 }
            ];

            _assignMomentService.AssignMoment(points, loads);

            points[0].MomentJump.Should().BeApproximately(0, epsilon);
            points[0].Moment.Should().BeApproximately(-1000, epsilon);
            points[500].MomentJump.Should().BeNull();
            points[500].Moment.Should().BeApproximately(-500, epsilon);
            points[1000].MomentJump.Should().BeNull();
            points[1000].Moment.Should().BeApproximately(0, epsilon);
        }

        [Fact]
        public void AssignMoment_TwoSupportsOneForce()
        {
            IMoment[] points = ArrangePoints(1001);

            Load[] loads =
            [
                new ForceLoad { Position = 0.1, Value = 1000 },
                new ForceLoad { Position = 0.5, Value = -2000 },
                new ForceLoad { Position = 0.9, Value = 1000 },
            ];

            _assignMomentService.AssignMoment(points, loads);

            points[0].MomentJump.Should().BeNull();
            points[0].Moment.Should().BeApproximately(0, epsilon);
            points[100].MomentJump.Should().BeNull();
            points[100].Moment.Should().BeApproximately(0, epsilon);
            points[300].MomentJump.Should().BeNull();
            points[300].Moment.Should().BeApproximately(200, epsilon);
            points[500].MomentJump.Should().BeNull();
            points[500].Moment.Should().BeApproximately(400, epsilon);
            points[700].MomentJump.Should().BeNull();
            points[700].Moment.Should().BeApproximately(200, epsilon);
            points[900].MomentJump.Should().BeNull();
            points[900].Moment.Should().BeApproximately(0, epsilon);
            points[1000].MomentJump.Should().BeNull();
            points[1000].Moment.Should().BeApproximately(0, epsilon);
        }

        [Fact]
        public void AssignMoment_OneSupportOneMomentAtEnd()
        {
            IMoment[] points = ArrangePoints(1001);

            Load[] loads =
            [
                new MomentLoad { Position = 0, Value = 1000 },
                new MomentLoad { Position = 1, Value = -1000 }
            ];

            _assignMomentService.AssignMoment(points, loads);

            points[0].MomentJump.Should().BeApproximately(0, epsilon);
            points[0].Moment.Should().BeApproximately(-1000, epsilon);
            points[500].MomentJump.Should().BeNull();
            points[500].Moment.Should().BeApproximately(-1000, epsilon);
            points[1000].MomentJump.Should().BeApproximately(-1000, epsilon);
            points[1000].Moment.Should().BeApproximately(0, epsilon);
        }

        [Fact]
        public void AssignMoment_TwoSupportsOneMoment()
        {
            IMoment[] points = ArrangePoints(1001);

            Load[] loads =
            [
                new ForceLoad { Position = 0.1, Value = -2500 },
                new MomentLoad { Position = 0.5, Value = -2000 },
                new ForceLoad { Position = 0.9, Value = 2500 }
            ];

            _assignMomentService.AssignMoment(points, loads);

            points[0].MomentJump.Should().BeNull();
            points[0].Moment.Should().BeApproximately(0, epsilon);
            points[100].MomentJump.Should().BeNull();
            points[100].Moment.Should().BeApproximately(0, epsilon);
            points[300].MomentJump.Should().BeNull();
            points[300].Moment.Should().BeApproximately(-500, epsilon);
            points[500].MomentJump.Should().BeApproximately(-1000, epsilon);
            points[500].Moment.Should().BeApproximately(1000, epsilon);
            points[700].MomentJump.Should().BeNull();
            points[700].Moment.Should().BeApproximately(500, epsilon);
            points[900].MomentJump.Should().BeNull();
            points[900].Moment.Should().BeApproximately(0, epsilon);
            points[1000].MomentJump.Should().BeNull();
            points[1000].Moment.Should().BeApproximately(0, epsilon);
        }

        [Fact]
        public void AssignMoment_OneSupportOneContinousLoad()
        {
            IMoment[] points = ArrangePoints(1001);

            Load[] loads =
            [
                new ForceLoad { Position = 0, Value = 1000 },
                new MomentLoad { Position = 0, Value = 500 },
                new ContinuousLoad { Position = 0, Length = 1, Value = -1000, StartCoefficient = 1, EndCoefficient = 1 }
            ];

            _assignMomentService.AssignMoment(points, loads);

            points[0].MomentJump.Should().BeApproximately(0, epsilon);
            points[0].Moment.Should().BeApproximately(-500, epsilon);
            points[500].MomentJump.Should().BeNull();
            points[500].Moment.Should().BeApproximately(-125, epsilon);
            points[1000].MomentJump.Should().BeNull();
            points[1000].Moment.Should().BeApproximately(0, epsilon);
        }

        [Fact]
        public void AssignMoment_TwoSupportsOneContinousLoad()
        {
            IMoment[] points = ArrangePoints(1001);

            Load[] loads =
            [
                new ForceLoad { Position = 0, Value = 1000 },
                new ForceLoad { Position = 1, Value = 1000 },
                new ContinuousLoad { Position = 0, Length = 1, Value = -2000, StartCoefficient = 1, EndCoefficient = 1 }
            ];

            _assignMomentService.AssignMoment(points, loads);

            points[0].MomentJump.Should().BeNull();
            points[0].Moment.Should().BeApproximately(0, epsilon);
            points[250].MomentJump.Should().BeNull();
            points[250].Moment.Should().BeApproximately(187.5, epsilon);
            points[500].MomentJump.Should().BeNull();
            points[500].Moment.Should().BeApproximately(250, epsilon);
            points[750].MomentJump.Should().BeNull();
            points[750].Moment.Should().BeApproximately(187.5, epsilon);
            points[1000].MomentJump.Should().BeNull();
            points[1000].Moment.Should().BeApproximately(0, epsilon);
        }
    }

    file class TestMoment : IMoment
    {
        public double Position { get; set; }
        public double Moment { get; set; }
        public double? MomentJump { get; set; }
    }
}

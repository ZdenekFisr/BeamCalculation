using Application.Input;
using Application.PointComponents;
using Application.Services.AssignStress;
using FluentAssertions;

namespace UnitTests
{
    public class AssignStressServiceTests
    {
        private readonly AssignStressService _assignStressService;

        public AssignStressServiceTests()
        {
            _assignStressService = new();
        }

        private void PerformAssignStressTest(double loadScale)
        {
            TestPoint[] points = new TestPoint[1001];
            double previousMoment = 0;
            for (int i = 0; i < points.Length; i++)
            {
                double moment = loadScale * (1000 - i / 200 * 200);

                points[i] = new TestPoint
                {
                    Position = (double)i / 1000,
                    Moment = moment,
                    MomentJump = previousMoment == moment ? null : previousMoment
                };

                previousMoment = moment;
            }

            Modulus[] moduli =
            [
                new Modulus { From = 0, Value = 25000 },
                new Modulus { From = 0.2, Value = 40000 },
                new Modulus { From = 0.6, Value = 20000 },
                new Modulus { From = 0.7, Value = 10000 }
            ];

            _assignStressService.AssignStress(points, moduli);

            points[0].StressJump.Should().Be(0);
            points[0].Stress.Should().Be(loadScale * 40);
            points[200].StressJump.Should().Be(loadScale * 40);
            points[200].Stress.Should().Be(loadScale * 20);
            points[600].StressJump.Should().Be(loadScale * 10);
            points[600].Stress.Should().Be(loadScale * 30);
            points[700].StressJump.Should().Be(loadScale * 20);
            points[700].Stress.Should().Be(loadScale * 40);
        }

        [Fact]
        public void AssignStress_AllCases_PositiveMoment()
            => PerformAssignStressTest(1);

        [Fact]
        public void AssignStress_AllCases_NegativeMoment()
            => PerformAssignStressTest(-1);
    }

    public class TestPoint : IStress
    {
        public double Position { get; set; }
        public double Moment { get; set; }
        public double? MomentJump { get; set; }
        public double Stress { get; set; }
        public double? StressJump { get; set; }
    }
}

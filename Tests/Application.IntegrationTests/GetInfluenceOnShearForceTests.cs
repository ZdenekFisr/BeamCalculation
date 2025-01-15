using Application.Input;
using FluentAssertions;

namespace Application.IntegrationTests
{
    public class GetInfluenceOnShearForceTests
    {
        private const double floatingPointTolerance = 1e-10;

        private void PerformTest(Load load, double position, double expectedForce, double? expectedForceBeforeJump)
        {
            (double force, double? forceBeforeJump) = load.GetInfluenceOnShearForce(position);
            force.Should().BeApproximately(expectedForce, floatingPointTolerance);
            if (expectedForceBeforeJump.HasValue)
                forceBeforeJump.Should().BeApproximately(expectedForceBeforeJump.Value, floatingPointTolerance);
            else
                forceBeforeJump.Should().BeNull();
        }

        [Fact]
        public void GetInfluenceOnShearForce_ForceLoad_PositionBeforeLoad()
            => PerformTest(new ForceLoad { Position = 1, Value = 100 }, 0.5, 0, null);

        [Fact]
        public void GetInfluenceOnShearForce_ForceLoad_PositionAtLoad()
            => PerformTest(new ForceLoad { Position = 1, Value = 100 }, 1, 100, 100);

        [Fact]
        public void GetInfluenceOnShearForce_ForceLoad_PositionAfterLoad()
            => PerformTest(new ForceLoad { Position = 1, Value = 100 }, 1.5, 100, null);

        [Fact]
        public void GetInfluenceOnShearForce_MomentLoad_PositionBeforeLoad()
            => PerformTest(new MomentLoad { Position = 1, Value = 100 }, 0.5, 0, null);

        [Fact]
        public void GetInfluenceOnShearForce_MomentLoad_PositionAtLoad()
            => PerformTest(new MomentLoad { Position = 1, Value = 100 }, 1, 0, null);

        [Fact]
        public void GetInfluenceOnShearForce_MomentLoad_PositionAfterLoad()
            => PerformTest(new MomentLoad { Position = 1, Value = 100 }, 1.5, 0, null);

        [Fact]
        public void GetInfluenceOnShearForce_ContinuousLoad_PositionBeforeLoad()
        {
            ContinuousLoad continuousLoad = new()
            {
                Position = 1,
                Value = 100,
                Length = 1.2,
                StartCoefficient = 1,
                EndCoefficient = 1
            };
            PerformTest(continuousLoad, 0.5, 0, null);
        }

        [Fact]
        public void GetInfluenceOnShearForce_ContinuousLoad_PositionAtStart()
        {
            ContinuousLoad continuousLoad = new()
            {
                Position = 1,
                Value = 100,
                Length = 1.2,
                StartCoefficient = 1,
                EndCoefficient = 1
            };
            PerformTest(continuousLoad, 1, 0, null);
        }

        [Fact]
        public void GetInfluenceOnShearForce_ContinuousLoad_PositionInsideLoad()
        {
            ContinuousLoad continuousLoad = new()
            {
                Position = 1,
                Value = 100,
                Length = 1.2,
                StartCoefficient = 1,
                EndCoefficient = 1
            };
            PerformTest(continuousLoad, 1.6, 60, null);
        }

        [Fact]
        public void GetInfluenceOnShearForce_ContinuousLoad_PositionAtEnd()
        {
            ContinuousLoad continuousLoad = new()
            {
                Position = 1,
                Value = 100,
                Length = 1.2,
                StartCoefficient = 1,
                EndCoefficient = 1
            };
            PerformTest(continuousLoad, 2.2, 120, null);
        }

        [Fact]
        public void GetInfluenceOnShearForce_ContinuousLoad_PositionAfterLoad()
        {
            ContinuousLoad continuousLoad = new()
            {
                Position = 1,
                Value = 100,
                Length = 1.2,
                StartCoefficient = 1,
                EndCoefficient = 1
            };
            PerformTest(continuousLoad, 2.5, 120, null);
        }
    }
}

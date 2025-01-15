using Application.Input;
using FluentAssertions;

namespace Application.IntegrationTests
{
    public class GetInfluenceOnBendingMomentTests
    {
        private const double floatingPointTolerance = 1e-10;

        private void PerformTest(Load load, double position, double expectedMoment, double? expectedMomentBeforeJump)
        {
            (double moment, double? momentBeforeJump) = load.GetInfluenceOnBendingMoment(position);
            moment.Should().BeApproximately(expectedMoment, floatingPointTolerance);
            if (expectedMomentBeforeJump.HasValue)
                momentBeforeJump.Should().BeApproximately(expectedMomentBeforeJump.Value, floatingPointTolerance);
            else
                momentBeforeJump.Should().BeNull();
        }

        [Fact]
        public void GetInfluenceOnBendingMoment_ForceLoad_PositionBeforeLoad()
            => PerformTest(new ForceLoad { Position = 1, Value = 100 }, 0.5, 0, null);

        [Fact]
        public void GetInfluenceOnBendingMoment_ForceLoad_PositionAtLoad()
            => PerformTest(new ForceLoad { Position = 1, Value = 100 }, 1, 0, null);

        [Fact]
        public void GetInfluenceOnBendingMoment_ForceLoad_PositionAfterLoad()
            => PerformTest(new ForceLoad { Position = 1, Value = 100 }, 1.5, 50, null);

        [Fact]
        public void GetInfluenceOnBendingMoment_MomentLoad_PositionBeforeLoad()
            => PerformTest(new MomentLoad { Position = 1, Value = 100 }, 0.5, 0, null);

        [Fact]
        public void GetInfluenceOnBendingMoment_MomentLoad_PositionAtLoad()
            => PerformTest(new MomentLoad { Position = 1, Value = 100 }, 1, -100, -100);

        [Fact]
        public void GetInfluenceOnBendingMoment_MomentLoad_PositionAfterLoad()
            => PerformTest(new MomentLoad { Position = 1, Value = 100 }, 1.5, -100, null);

        [Fact]
        public void GetInfluenceOnBendingMoment_ContinuousLoad_PositionBeforeLoad()
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
        public void GetInfluenceOnBendingMoment_ContinuousLoad_PositionAtStart()
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
        public void GetInfluenceOnBendingMoment_ContinuousLoad_PositionInsideLoad()
        {
            ContinuousLoad continuousLoad = new()
            {
                Position = 1,
                Value = 100,
                Length = 1.2,
                StartCoefficient = 1,
                EndCoefficient = 1
            };
            PerformTest(continuousLoad, 1.6, 18, null);
        }

        [Fact]
        public void GetInfluenceOnBendingMoment_ContinuousLoad_PositionAtEnd()
        {
            ContinuousLoad continuousLoad = new()
            {
                Position = 1,
                Value = 100,
                Length = 1.2,
                StartCoefficient = 1,
                EndCoefficient = 1
            };
            PerformTest(continuousLoad, 2.2, 72, null);
        }

        [Fact]
        public void GetInfluenceOnBendingMoment_ContinuousLoad_PositionAfterLoad()
        {
            ContinuousLoad continuousLoad = new()
            {
                Position = 1,
                Value = 100,
                Length = 1.2,
                StartCoefficient = 1,
                EndCoefficient = 1
            };
            PerformTest(continuousLoad, 2.5, 108, null);
        }
    }
}

using Application.Input;
using FluentAssertions;

namespace Application.UnitTests
{
    public class GetSimplifiedLoadsTests
    {
        private const double floatingPointTolerance = 1e-10;

        [Fact]
        public void GetSimplifiedLoads_Force()
        {
            ForceLoad load = new()
            {
                Position = 0,
                Value = 1000
            };

            Load[] expected = [new ForceLoad { Position = 0, Value = 1000 }];
            Load[] actual = [.. load.GetSimplifiedLoads()];

            actual.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, floatingPointTolerance)).WhenTypeIs<double>());
        }

        [Fact]
        public void GetSimplifiedLoads_Moment()
        {
            MomentLoad load = new()
            {
                Position = 0,
                Value = 1000
            };

            Load[] expected = [new MomentLoad { Position = 0, Value = 1000 }];
            Load[] actual = [.. load.GetSimplifiedLoads()];

            actual.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, floatingPointTolerance)).WhenTypeIs<double>());
        }

        [Fact]
        public void GetSimplifiedLoads_ContinuousLoadConstant()
        {
            ContinuousLoad load = new()
            {
                Position = 0,
                Value = 1000,
                Length = 1.2,
                StartCoefficient = 1,
                EndCoefficient = 1
            };

            Load[] expected = [new ForceLoad { Position = 0.6, Value = 1200 }];
            Load[] actual = [.. load.GetSimplifiedLoads()];

            actual.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, floatingPointTolerance)).WhenTypeIs<double>());
        }

        [Fact]
        public void GetSimplifiedLoads_ContinuousLoadRisingFromZero()
        {
            ContinuousLoad load = new()
            {
                Position = 0,
                Value = 1000,
                Length = 1.2,
                StartCoefficient = 0,
                EndCoefficient = 1
            };

            Load[] expected = [new ForceLoad { Position = 0.8, Value = 600 }];
            Load[] actual = [.. load.GetSimplifiedLoads()];

            actual.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, floatingPointTolerance)).WhenTypeIs<double>());
        }

        [Fact]
        public void GetSimplifiedLoads_ContinuousLoadFallingToZero()
        {
            ContinuousLoad load = new()
            {
                Position = 0,
                Value = 1000,
                Length = 1.2,
                StartCoefficient = 1,
                EndCoefficient = 0
            };

            Load[] expected = [new ForceLoad { Position = 0.4, Value = 600 }];
            Load[] actual = [.. load.GetSimplifiedLoads()];

            actual.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, floatingPointTolerance)).WhenTypeIs<double>());
        }

        [Fact]
        public void GetSimplifiedLoads_ContinuousLoadZero()
        {
            ContinuousLoad load = new()
            {
                Position = 0,
                Value = 1000,
                Length = 1.2,
                StartCoefficient = 0,
                EndCoefficient = 0
            };

            Load[] actual = [.. load.GetSimplifiedLoads()];
            actual.Should().BeEmpty();
        }

        [Fact]
        public void GetSimplifiedLoads_ContinuousLoadRising()
        {
            ContinuousLoad load = new()
            {
                Position = 0,
                Value = 1000,
                Length = 1.2,
                StartCoefficient = 0.5,
                EndCoefficient = 1
            };

            Load[] expected = [new ForceLoad { Position = 0.6, Value = 600 }, new ForceLoad { Position = 0.8, Value = 300 }];
            Load[] actual = [.. load.GetSimplifiedLoads()];

            actual.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, floatingPointTolerance)).WhenTypeIs<double>());
        }

        [Fact]
        public void GetSimplifiedLoads_ContinuousLoadFalling()
        {
            ContinuousLoad load = new()
            {
                Position = 0,
                Value = 1000,
                Length = 1.2,
                StartCoefficient = 1,
                EndCoefficient = 0.5
            };

            Load[] expected = [new ForceLoad { Position = 0.6, Value = 600 }, new ForceLoad { Position = 0.4, Value = 300 }];
            Load[] actual = [.. load.GetSimplifiedLoads()];

            actual.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, floatingPointTolerance)).WhenTypeIs<double>());
        }

        [Fact]
        public void GetSimplifiedLoads_ContinuousLoadNegativeConstant()
        {
            ContinuousLoad load = new()
            {
                Position = 0,
                Value = 1000,
                Length = 1.2,
                StartCoefficient = -1,
                EndCoefficient = -1
            };

            Load[] expected = [new ForceLoad { Position = 0.6, Value = -1200 }];
            Load[] actual = [.. load.GetSimplifiedLoads()];

            actual.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, floatingPointTolerance)).WhenTypeIs<double>());
        }

        [Fact]
        public void GetSimplifiedLoads_ContinuousLoadNegativeRising()
        {
            ContinuousLoad load = new()
            {
                Position = 0,
                Value = 1000,
                Length = 1.2,
                StartCoefficient = -0.5,
                EndCoefficient = -1
            };

            Load[] expected = [new ForceLoad { Position = 0.6, Value = -600 }, new ForceLoad { Position = 0.8, Value = -300 }];
            Load[] actual = [.. load.GetSimplifiedLoads()];

            actual.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, floatingPointTolerance)).WhenTypeIs<double>());
        }

        [Fact]
        public void GetSimplifiedLoads_ContinuousLoadNegativeFalling()
        {
            ContinuousLoad load = new()
            {
                Position = 0,
                Value = 1000,
                Length = 1.2,
                StartCoefficient = -1,
                EndCoefficient = -0.5
            };

            Load[] expected = [new ForceLoad { Position = 0.6, Value = -600 }, new ForceLoad { Position = 0.4, Value = -300 }];
            Load[] actual = [.. load.GetSimplifiedLoads()];

            actual.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, floatingPointTolerance)).WhenTypeIs<double>());
        }

        [Fact]
        public void GetSimplifiedLoads_ContinuousLoadRisingThroughZero()
        {
            ContinuousLoad load = new()
            {
                Position = 0,
                Value = 1000,
                Length = 1.2,
                StartCoefficient = -0.5,
                EndCoefficient = 1.5
            };

            Load[] expected = [new ForceLoad { Position = 0.1, Value = -75 }, new ForceLoad { Position = 0.9, Value = 675 }];
            Load[] actual = [.. load.GetSimplifiedLoads()];

            actual.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, floatingPointTolerance)).WhenTypeIs<double>());
        }

        [Fact]
        public void GetSimplifiedLoads_ContinuousLoadFallingThroughZero()
        {
            ContinuousLoad load = new()
            {
                Position = 0,
                Value = 1000,
                Length = 1.2,
                StartCoefficient = 1.5,
                EndCoefficient = -0.5
            };
            Load[] expected = [new ForceLoad { Position = 0.3, Value = 675 }, new ForceLoad { Position = 1.1, Value = -75 }];
            Load[] actual = [.. load.GetSimplifiedLoads()];
            actual.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, floatingPointTolerance)).WhenTypeIs<double>());
        }
    }
}

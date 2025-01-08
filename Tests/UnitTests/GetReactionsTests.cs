using Application.Input;
using FluentAssertions;

namespace UnitTests
{
    public class GetReactionsTests
    {
        private void PerformTestForOneSupport(ICollection<Load> loads, double expectedReactionMA, double expectedReactionAY)
        {
            const double supportA = 0;

            OneSupportBeam beam = new()
            {
                Length = 1,
                SupportA = supportA,
                Loads = loads,
                Moduli = []
            };

            Load[] expected =
            [
                new MomentLoad { Value = expectedReactionMA, Position = supportA },
                new ForceLoad { Value = expectedReactionAY, Position = supportA }
            ];
            Load[] actual = [.. beam.GetReactions()];

            actual.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, 1e-6)).WhenTypeIs<double>());
        }

        [Fact]
        public void CalculateReactions_OneSupport_ZeroLoad()
            => PerformTestForOneSupport([], 0, 0);

        [Fact]
        public void CalculateReactions_OneSupport_ForceLoadAtStart()
            => PerformTestForOneSupport([new ForceLoad { Value = -1000, Position = 0 }], 1000, 0);

        [Fact]
        public void CalculateReactions_OneSupport_ForceLoadAtHalf()
            => PerformTestForOneSupport([new ForceLoad { Value = -1000, Position = 0.5 }], 1000, 500);

        [Fact]
        public void CalculateReactions_OneSupport_ForceLoadAtEnd()
            => PerformTestForOneSupport([new ForceLoad { Value = -1000, Position = 1 }], 1000, 1000);

        [Fact]
        public void CalculateReactions_OneSupport_MomentLoad()
            => PerformTestForOneSupport([new MomentLoad { Value = 2000, Position = 0.5 }], 0, -2000);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadConstantAtStart()
            => PerformTestForOneSupport([new ContinuousLoad { Value = -1000, Position = 0, Length = 0.5, StartCoefficient = 1, EndCoefficient = 1 }], 500, 125);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadConstantAtEnd()
            => PerformTestForOneSupport([new ContinuousLoad { Value = -1000, Position = 0.5, Length = 0.5, StartCoefficient = 1, EndCoefficient = 1 }], 500, 375);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadConstantOverlaps()
            => PerformTestForOneSupport([new ContinuousLoad { Value = -1000, Position = 0, Length = 1.5, StartCoefficient = 1, EndCoefficient = 1 }], 1500, 1125);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadRisingAtStart()
            => PerformTestForOneSupport([new ContinuousLoad { Value = -1000, Position = 0, Length = 0.6, StartCoefficient = 0, EndCoefficient = 1 }], 300, 120);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadRisingAtEnd()
            => PerformTestForOneSupport([new ContinuousLoad { Value = -1000, Position = 0.4, Length = 0.6, StartCoefficient = 0, EndCoefficient = 1 }], 300, 240);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadRisingOverlaps()
            => PerformTestForOneSupport([new ContinuousLoad { Value = -1000, Position = 0, Length = 1.2, StartCoefficient = 0, EndCoefficient = 1 }], 600, 480);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadFallingAtStart()
            => PerformTestForOneSupport([new ContinuousLoad { Value = -1000, Position = 0, Length = 0.6, StartCoefficient = 1, EndCoefficient = 0 }], 300, 60);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadFallingAtEnd()
            => PerformTestForOneSupport([new ContinuousLoad { Value = -1000, Position = 0.4, Length = 0.6, StartCoefficient = 1, EndCoefficient = 0 }], 300, 180);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadFallingOverlaps()
            => PerformTestForOneSupport([new ContinuousLoad { Value = -1000, Position = 0, Length = 1.2, StartCoefficient = 1, EndCoefficient = 0 }], 600, 240);

        [Fact]
        public void CalculateReactions_OneSupport_ManyLoads()
        {
            List<Load> loads =
            [
                new ForceLoad { Value = -2000, Position = 0 },
                new ForceLoad { Value = -2000, Position = 1 },
                new MomentLoad { Value = 3000, Position = 0.5 },
                new ContinuousLoad { Value = -500, Position = 0, Length = 1, StartCoefficient = 1, EndCoefficient = 1 }, // 4500, -750
                new ContinuousLoad { Value = -1000, Position = 0.7, Length = 0.3, StartCoefficient = 0, EndCoefficient = 1 }, // 4650, -615
                new ContinuousLoad { Value = -1000, Position = 0, Length = 0.3, StartCoefficient = 1, EndCoefficient = 0 } // 4800, -600
            ];
            PerformTestForOneSupport(loads, 4800, -600);
        }

        private void PerformTestForTwoSupports(ICollection<Load> loads, double expectedReactionAY, double expectedReactionBY)
        {
            const double supportA = 0.1;
            const double supportB = 1.1;

            TwoSupportsBeam beam = new()
            {
                Length = 1.2,
                SupportA = supportA,
                SupportB = supportB,
                Loads = loads,
                Moduli = []
            };

            Load[] expected =
            [
                new ForceLoad { Value = expectedReactionAY, Position = supportA },
                new ForceLoad { Value = expectedReactionBY, Position = supportB }
            ];
            Load[] actual = [.. beam.GetReactions()];

            actual.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, 1e-6)).WhenTypeIs<double>());
        }

        [Fact]
        public void CalculateReactions_TwoSupports_ZeroLoad()
            => PerformTestForTwoSupports([], 0, 0);

        [Fact]
        public void CalculateReactions_TwoSupports_ForceLoadAtHalf()
            => PerformTestForTwoSupports([new ForceLoad { Value = -1000, Position = 0.6 }], 500, 500);

        [Fact]
        public void CalculateReactions_TwoSupports_ForceLoadUnbalanced()
            => PerformTestForTwoSupports([new ForceLoad { Value = -1000, Position = 0.35 }], 750, 250);

        [Fact]
        public void CalculateReactions_TwoSupports_ForceLoadsAtEdges()
        {
            List<Load> loads =
            [
                new ForceLoad { Value = -1000, Position = 0 },
                new ForceLoad { Value = -1000, Position = 1.2 }
            ];

            PerformTestForTwoSupports(loads, 1000, 1000);
        }

        [Fact]
        public void CalculateReactions_TwoSupports_MomentLoad()
            => PerformTestForTwoSupports([new MomentLoad { Value = 2000, Position = 0.4 }], 2000, -2000);

        [Fact]
        public void CalculateReactions_TwoSupports_ContinuousLoadConstantThrough()
            => PerformTestForTwoSupports([new ContinuousLoad { Value = -1000, Position = 0, Length = 1.2, StartCoefficient = 1, EndCoefficient = 1 }], 600, 600);

        [Fact]
        public void CalculateReactions_TwoSupports_ContinuousLoadConstantAtStart()
            => PerformTestForTwoSupports([new ContinuousLoad { Value = -1000, Position = 0, Length = 0.4, StartCoefficient = 1, EndCoefficient = 1 }], 360, 40);

        [Fact]
        public void CalculateReactions_TwoSupports_ContinuousLoadConstantAtMiddle()
            => PerformTestForTwoSupports([new ContinuousLoad { Value = -1000, Position = 0.4, Length = 0.4, StartCoefficient = 1, EndCoefficient = 1 }], 200, 200);

        [Fact]
        public void CalculateReactions_TwoSupports_ContinuousLoadConstantAtEnd()
            => PerformTestForTwoSupports([new ContinuousLoad { Value = -1000, Position = 0.8, Length = 0.4, StartCoefficient = 1, EndCoefficient = 1 }], 40, 360);

        [Fact]
        public void CalculateReactions_TwoSupports_ContinuousLoadRisingThrough()
            => PerformTestForTwoSupports([new ContinuousLoad { Value = -1000, Position = 0, Length = 1.2, StartCoefficient = 0, EndCoefficient = 1 }], 180, 420);

        [Fact]
        public void CalculateReactions_TwoSupports_ContinuousLoadFallingThrough()
            => PerformTestForTwoSupports([new ContinuousLoad { Value = -1000, Position = 0, Length = 1.2, StartCoefficient = 1, EndCoefficient = 0 }], 420, 180);

        [Fact]
        public void CalculateReactions_TwoSupports_ManyLoads()
        {
            List<Load> loads =
            [
                new ForceLoad { Value = -2000, Position = 0 },
                new ForceLoad { Value = -2000, Position = 1.2 },
                new MomentLoad { Value = 3000, Position = 0.4 },
                new ContinuousLoad { Value = -500, Position = 0.1, Length = 1, StartCoefficient = 1, EndCoefficient = 1 },
                new ContinuousLoad { Value = -1000, Position = 0.8, Length = 0.3, StartCoefficient = 0, EndCoefficient = 1 },
                new ContinuousLoad { Value = -1000, Position = 0.1, Length = 0.3, StartCoefficient = 1, EndCoefficient = 0 }
            ];
            PerformTestForTwoSupports(loads, 5400, -600);
        }
    }
}

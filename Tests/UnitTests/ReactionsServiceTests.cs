using Application.Enums;
using Application.Input;
using Application.Services.Reactions;
using FluentAssertions;

namespace UnitTests
{
    public class ReactionsServiceTests
    {
        private readonly ReactionsService _reactionsService;

        public ReactionsServiceTests()
        {
            _reactionsService = new();
        }

        private void PerformTestForOneSupport(ICollection<Load> loads, double expectedReaction1, double expectedReaction2)
        {
            Beam beam = new()
            {
                Type = BeamType.OneSupport,
                Length = 1,
                Loads = loads,
                Moduli = []
            };

            (double reaction1, double reaction2) actual = _reactionsService.CalculateReactions(beam);
            actual.reaction1 = Math.Round(actual.reaction1, 4);
            actual.reaction2 = Math.Round(actual.reaction2, 4);

            actual.Should().Be((expectedReaction1, expectedReaction2));
        }

        [Fact]
        public void CalculateReactions_OneSupport_ZeroLoad()
            => PerformTestForOneSupport([], 0, 0);

        [Fact]
        public void CalculateReactions_OneSupport_ForceLoadAtStart()
            => PerformTestForOneSupport([new() { Type = LoadType.Force, Value = -1000, Position = 0 }], 1000, 0);

        [Fact]
        public void CalculateReactions_OneSupport_ForceLoadAtHalf()
            => PerformTestForOneSupport([new() { Type = LoadType.Force, Value = -1000, Position = 0.5 }], 1000, 500);

        [Fact]
        public void CalculateReactions_OneSupport_ForceLoadAtEnd()
            => PerformTestForOneSupport([new() { Type = LoadType.Force, Value = -1000, Position = 1 }], 1000, 1000);

        [Fact]
        public void CalculateReactions_OneSupport_MomentLoad()
            => PerformTestForOneSupport([new() { Type = LoadType.Moment, Value = 2000, Position = 0.5 }], 0, -2000);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadConstantAtStart()
            => PerformTestForOneSupport([new() { Type = LoadType.ContinuousLoadConstant, Value = -1000, Position = 0, Length = 0.5 }], 500, 125);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadConstantAtEnd()
            => PerformTestForOneSupport([new() { Type = LoadType.ContinuousLoadConstant, Value = -1000, Position = 0.5, Length = 0.5 }], 500, 375);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadConstantOverlaps()
            => PerformTestForOneSupport([new() { Type = LoadType.ContinuousLoadConstant, Value = -1000, Position = 0, Length = 1.5 }], 1500, 1125);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadRisingAtStart()
            => PerformTestForOneSupport([new() { Type = LoadType.ContinuousLoadRising, Value = -1000, Position = 0, Length = 0.6 }], 300, 120);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadRisingAtEnd()
            => PerformTestForOneSupport([new() { Type = LoadType.ContinuousLoadRising, Value = -1000, Position = 0.4, Length = 0.6 }], 300, 240);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadRisingOverlaps()
            => PerformTestForOneSupport([new() { Type = LoadType.ContinuousLoadRising, Value = -1000, Position = 0, Length = 1.2 }], 600, 480);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadFallingAtStart()
            => PerformTestForOneSupport([new() { Type = LoadType.ContinuousLoadFalling, Value = -1000, Position = 0, Length = 0.6 }], 300, 60);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadFallingAtEnd()
            => PerformTestForOneSupport([new() { Type = LoadType.ContinuousLoadFalling, Value = -1000, Position = 0.4, Length = 0.6 }], 300, 180);

        [Fact]
        public void CalculateReactions_OneSupport_ContinuousLoadFallingOverlaps()
            => PerformTestForOneSupport([new() { Type = LoadType.ContinuousLoadFalling, Value = -1000, Position = 0, Length = 1.2 }], 600, 240);

        [Fact]
        public void CalculateReactions_OneSupport_ManyLoads()
        {
            List<Load> loads =
            [
                new() { Type = LoadType.Force, Value = -2000, Position = 0 }, // 2000, 0
                new() { Type = LoadType.Force, Value = -2000, Position = 1 }, // 4000, 2000
                new() { Type = LoadType.Moment, Value = 3000, Position = 0.5 }, // 4000, -1000
                new() { Type = LoadType.ContinuousLoadConstant, Value = -500, Position = 0, Length = 1 }, // 4500, -750
                new() { Type = LoadType.ContinuousLoadRising, Value = -1000, Position = 0.7, Length = 0.3 }, // 4650, -615
                new() { Type = LoadType.ContinuousLoadFalling, Value = -1000, Position = 0, Length = 0.3 } // 4800, -600
            ];
            PerformTestForOneSupport(loads, 4800, -600);
        }
    }
}

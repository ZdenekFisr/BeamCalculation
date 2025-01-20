using Application.Exceptions;
using Application.Input;
using Application.Services.AssignForce;
using Application.Services.AssignMoment;
using Application.Services.AssignStress;
using Application.Services.Calculation;
using Application.Services.CreatePoints;
using FluentAssertions;

namespace Application.IntegrationTests
{
    public class CalculationServiceTests
    {
        private readonly CalculationService _calculationService;

        public CalculationServiceTests()
        {
            _calculationService = new(
                new CreatePointsService(),
                new AssignForceService(),
                new AssignMomentService(),
                new AssignStressService());
        }

        [Fact]
        public void Calculate_ZeroModulus_ThrowsInvalidModulusException()
        {
            OneSupportBeam beam = new()
            {
                Length = 10,
                Moduli =
                [
                    new Modulus { From = 0, Value = 0 }
                ],
                Loads = []
            };
            _calculationService.Invoking(s => s.Calculate(beam, 0.001))
                .Should().Throw<InvalidModulusException>();
        }

        [Fact]
        public void Calculate_NegativeModulus_ThrowsInvalidModulusException()
        {
            OneSupportBeam beam = new()
            {
                Length = 10,
                Moduli =
                [
                    new Modulus { From = 0, Value = -1000 }
                ],
                Loads = []
            };

            _calculationService.Invoking(s => s.Calculate(beam, 0.001))
                .Should().Throw<InvalidModulusException>();
        }

        [Fact]
        public void Calculate_LoadOutsideOfBeamLeft_ThrowsLoadOutsideOfBeamException()
        {
            OneSupportBeam beam = new()
            {
                Length = 10,
                Moduli =
                [
                    new Modulus { From = 0, Value = 25000 }
                ],
                Loads =
                [
                    new ForceLoad { Position = -1, Value = 10 }
                ]
            };
            _calculationService.Invoking(s => s.Calculate(beam, 0.001))
                .Should().Throw<LoadOutsideOfBeamException>();
        }

        [Fact]
        public void Calculate_LoadOutsideOfBeamRight_ThrowsLoadOutsideOfBeamException()
        {
            OneSupportBeam beam = new()
            {
                Length = 10,
                Moduli =
                [
                    new Modulus { From = 0, Value = 25000 }
                ],
                Loads =
                [
                    new ForceLoad { Position = 20, Value = 10 }
                ]
            };
            _calculationService.Invoking(s => s.Calculate(beam, 0.001))
                .Should().Throw<LoadOutsideOfBeamException>();
        }

        [Fact]
        public void Calculate_ContinuousLoadExceedsBeamLeft_ThrowsContinuousLoadExceedsBeamException()
        {
            OneSupportBeam beam = new()
            {
                Length = 10,
                Moduli =
                [
                    new Modulus { From = 0, Value = 25000 }
                ],
                Loads =
                [
                    new ContinuousLoad { Position = 0, Length = -2, Value = 10 }
                ]
            };
            _calculationService.Invoking(s => s.Calculate(beam, 0.001))
                .Should().Throw<ContinuousLoadExceedsBeamException>();
        }

        [Fact]
        public void Calculate_ContinuousLoadExceedsBeamRight_ThrowsContinuousLoadExceedsBeamException()
        {
            OneSupportBeam beam = new()
            {
                Length = 10,
                Moduli =
                [
                    new Modulus { From = 0, Value = 25000 }
                ],
                Loads =
                [
                    new ContinuousLoad { Position = 9, Length = 2, Value = 10 }
                ]
            };
            _calculationService.Invoking(s => s.Calculate(beam, 0.001))
                .Should().Throw<ContinuousLoadExceedsBeamException>();
        }

        [Fact]
        public void Calculate_ZeroBeamLength_ThrowsInvalidBeamLengthException()
        {
            OneSupportBeam beam = new()
            {
                Length = 0,
                Moduli =
                [
                    new Modulus { From = 0, Value = 25000 }
                ],
                Loads = []
            };
            _calculationService.Invoking(s => s.Calculate(beam, 0.001))
                .Should().Throw<InvalidBeamLengthException>();
        }

        [Fact]
        public void Calculate_NegativeBeamLength_ThrowsInvalidBeamLengthException()
        {
            OneSupportBeam beam = new()
            {
                Length = -10,
                Moduli =
                [
                    new Modulus { From = 0, Value = 25000 }
                ],
                Loads = []
            };
            _calculationService.Invoking(s => s.Calculate(beam, 0.001))
                .Should().Throw<InvalidBeamLengthException>();
        }

        [Fact]
        public void Calculate_ZeroLengthBetweenPoints_ThrowsInvalidLengthBetweenPointsException()
        {
            OneSupportBeam beam = new()
            {
                Length = 10,
                Moduli =
                [
                    new Modulus { From = 0, Value = 25000 }
                ],
                Loads = []
            };
            _calculationService.Invoking(s => s.Calculate(beam, 0))
                .Should().Throw<InvalidLengthBetweenPointsException>();
        }

        [Fact]
        public void Calculate_NegativeLengthBetweenPoints_ThrowsInvalidLengthBetweenPointsException()
        {
            OneSupportBeam beam = new()
            {
                Length = 10,
                Moduli =
                [
                    new Modulus { From = 0, Value = 25000 }
                ],
                Loads = []
            };
            _calculationService.Invoking(s => s.Calculate(beam, -0.001))
                .Should().Throw<InvalidLengthBetweenPointsException>();
        }

        [Fact]
        public void Calculate_ZeroModuli_ThrowsModulusNotDefinedException()
        {
            OneSupportBeam beam = new()
            {
                Length = 10,
                Moduli = [],
                Loads = []
            };
            _calculationService.Invoking(s => s.Calculate(beam, 0.001))
                .Should().Throw<ModulusNotDefinedException>();
        }

        [Fact]
        public void Calculate_ModuliDoNotCoverAllPoints_ThrowsModulusNotDefinedException()
        {
            OneSupportBeam beam = new()
            {
                Length = 10,
                Moduli =
                [
                    new Modulus { From = 0.2, Value = 40000 }
                ],
                Loads = []
            };
            _calculationService.Invoking(s => s.Calculate(beam, 0.001))
                .Should().Throw<ModulusNotDefinedException>();
        }
    }
}

using Application.Input;
using Application.Services.AssignForce;
using Application.Services.AssignMoment;
using Application.Services.AssignStress;
using Application.Services.CreatePoints;

namespace Application.Services.Calculation
{
    public class CalculationService(
        ICreatePointsService createPointsService,
        IAssignForceService assignForceService,
        IAssignMomentService assignMomentService,
        IAssignStressService assignStressService)
        : ICalculationService
    {
        private readonly ICreatePointsService _createPointsService = createPointsService;
        private readonly IAssignForceService _assignForceService = assignForceService;
        private readonly IAssignMomentService _assignMomentService = assignMomentService;
        private readonly IAssignStressService _assignStressService = assignStressService;

        public void Calculate(Beam beam, double lengthBetweenPoints)
        {
            Load[] reactions = [.. beam.GetReactions()];
            Load[] totalLoads = [.. beam.Loads, .. reactions];

            Point[] points = _createPointsService.CreatePoints(beam.Length, lengthBetweenPoints);
            _assignForceService.AssignForce(points, totalLoads);
            _assignMomentService.AssignMoment(points, totalLoads);
            _assignStressService.AssignStress(points, beam.Moduli);
        }
    }
}

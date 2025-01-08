using Application.Input;
using Application.Services.AssignForce;
using Application.Services.CreatePoints;

namespace Application.Services.Calculation
{
    public class CalculationService(
        ICreatePointsService createPointsService,
        IAssignForceService assignForceService)
        : ICalculationService
    {
        private readonly ICreatePointsService _createPointsService = createPointsService;
        private readonly IAssignForceService _assignForceService = assignForceService;

        public void Calculate(Beam beam, double lengthBetweenPoints)
        {
            Load[] reactions = [.. beam.GetReactions()];
            Load[] totalLoads = [.. beam.Loads, .. reactions];

            Point[] points = _createPointsService.CreatePoints(beam.Length, lengthBetweenPoints);
            _assignForceService.AssignForce(points, totalLoads);
        }
    }
}

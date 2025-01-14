using Application.Exceptions;
using Application.Input;
using Application.Services.AssignForce;
using Application.Services.AssignMoment;
using Application.Services.AssignStress;
using Application.Services.CreatePoints;

namespace Application.Services.Calculation
{
    /// <summary>
    /// Service for performing calculations on a beam.
    /// </summary>
    /// <param name="createPointsService">Service for creating points on the beam.</param>
    /// <param name="assignForceService">Service for assigning shear force to points.</param>
    /// <param name="assignMomentService">Service for assigning bending moment to points.</param>
    /// <param name="assignStressService">Service for assigning mechanical stress in bending to points.</param>
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

        /// <inheritdoc />
        public Output Calculate(Beam beam, double lengthBetweenPoints)
        {
            if (beam.Moduli.Any(m => m.Value <= 0))
                throw new InvalidModulusException();

            if (beam.Loads.Any(l => l.Position < 0 || l.Position > beam.Length))
                throw new LoadOutsideOfBeamException();

            if (beam.Loads.Any(l => l is ContinuousLoad cl && cl.Position + cl.Length > beam.Length))
                throw new ContinuousLoadExceedsBeamException();

            Load[] reactions = [.. beam.GetReactions()];
            Load[] totalLoads = [.. beam.Loads, .. reactions];

            Point[] points = _createPointsService.CreatePoints(beam.Length, lengthBetweenPoints);
            _assignForceService.AssignForce(points, totalLoads);
            _assignMomentService.AssignMoment(points, totalLoads);
            _assignStressService.AssignStress(points, beam.Moduli);

            return new()
            {
                Points = points,
                Reaction1 = reactions[0],
                Reaction2 = reactions[1]
            };
        }
    }
}

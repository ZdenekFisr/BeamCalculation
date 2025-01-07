using Application.Input;
using Application.Services.CreateLoads;
using Application.Services.CreatePoints;
using Application.Services.Reactions;

namespace Application.Services.Calculation
{
    public class CalculationService(
        IReactionsService reactionsService,
        ICreateLoadsService createLoadsService,
        ICreatePointsService createPointsService) : ICalculationService
    {
        private readonly IReactionsService _reactionsService = reactionsService;
        private readonly ICreateLoadsService _createLoadsService = createLoadsService;
        private readonly ICreatePointsService _createPointsService = createPointsService;

        public void Calculate(Beam beam, double lengthBetweenPoints)
        {
            (double reaction1, double reaction2) = _reactionsService.CalculateReactions(beam);
            Load[] loads = _createLoadsService.CreateLoads(beam.Loads, reaction1, reaction2, beam.TotalLength, beam.OverlapA);
            Point[] points = _createPointsService.CreatePoints(beam.TotalLength, lengthBetweenPoints);
        }
    }
}

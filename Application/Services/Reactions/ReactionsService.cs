using Application.Enums;
using Application.Input;

namespace Application.Services.Reactions
{
    /// <inheritdoc cref="IReactionsService"/>
    public class ReactionsService : IReactionsService
    {
        /// <inheritdoc />
        public (double reaction1, double reaction2) CalculateReactions(Beam beam)
        {
            List<Load> loads = [];

            foreach (Load load in beam.Loads)
            {
                loads.AddRange(load.GetSimplifiedLoads());
            }

            double reactionMA = 0, reactionAY = 0, reactionBY;
            switch (beam.Type)
            {
                case BeamType.OneSupport:
                    reactionMA = loads.Where(l => l is MomentLoad).Sum(l => -l.Value) + loads.Where(l => l is ForceLoad).Sum(l => -l.Value * l.Position);
                    reactionAY = loads.Where(l => l is ForceLoad).Sum(l => -l.Value);
                    return (reactionAY, reactionMA);

                case BeamType.TwoSupports:
                    reactionMA = loads.Where(l => l is MomentLoad).Sum(l => -l.Value) + loads.Where(l => l is ForceLoad).Sum(l => -l.Value * (l.Position - beam.OverlapA));
                    reactionBY = reactionMA / beam.Length;
                    loads.Add(new ForceLoad
                    {
                        Value = reactionBY,
                        Position = beam.Length + beam.OverlapA
                    });
                    reactionAY = loads.Where(l => l is ForceLoad).Sum(l => -l.Value);
                    return (reactionAY, reactionBY);

                default:
                    throw new NotImplementedException("This type of beam is not implemented.");
            }
        }
    }
}

using Application.Enums;
using Application.Input;

namespace Application.Services.Reactions
{
    public class ReactionsService : IReactionsService
    {
        /// <inheritdoc />
        public (double reaction1, double reaction2) CalculateReactions(Beam beam)
        {
            List<Load> loads = [];

            foreach (Load load in beam.Loads)
            {
                switch (load.Type)
                {
                    case LoadType.Force:
                    case LoadType.Moment:
                        loads.Add(load);
                        break;

                    case LoadType.ContinuousLoadConstant:
                        loads.Add(new()
                        {
                            Value = load.Value * load.Length,
                            Type = LoadType.Force,
                            Position = load.Position + load.Length / 2
                        });
                        break;

                    case LoadType.ContinuousLoadRising:
                        loads.Add(new()
                        {
                            Value = load.Value * load.Length / 2,
                            Type = LoadType.Force,
                            Position = load.Position + 2 * load.Length / 3
                        });
                        break;

                    case LoadType.ContinuousLoadFalling:
                        loads.Add(new()
                        {
                            Value = load.Value * load.Length / 2,
                            Type = LoadType.Force,
                            Position = load.Position + load.Length / 3
                        });
                        break;
                }
            }

            double reactionMA = 0, reactionAY = 0, reactionBY;
            switch (beam.Type)
            {
                case BeamType.OneSupport:
                    reactionMA = loads.Where(l => l.Type == LoadType.Moment).Sum(l => -l.Value) + loads.Where(l => l.Type == LoadType.Force).Sum(l => -l.Value * l.Position);
                    reactionAY = loads.Where(l => l.Type == LoadType.Force).Sum(l => -l.Value);
                    return (reactionAY, reactionMA);

                case BeamType.TwoSupports:
                    reactionMA = loads.Where(l => l.Type == LoadType.Moment).Sum(l => -l.Value) + loads.Where(l => l.Type == LoadType.Force).Sum(l => -l.Value * (l.Position - beam.OverlapA));
                    reactionBY = reactionMA / beam.Length;
                    loads.Add(new()
                    {
                        Value = reactionBY,
                        Type = LoadType.Force,
                        Position = beam.Length + beam.OverlapA,
                        Length = 0
                    });
                    reactionAY = loads.Where(l => l.Type == LoadType.Force).Sum(l => -l.Value);
                    return (reactionAY, reactionBY);

                default:
                    throw new NotImplementedException("This type of beam is not implemented.");
            }
        }
    }
}

namespace Application.Input
{
    public class TwoSupportsBeam : Beam
    {
        /// <summary>
        /// The position of support A from the left edge of the beam in meters. This support allows rotation.
        /// </summary>
        public override double SupportA { get; set; }

        /// <summary>
        /// The position of support B from the left edge of the beam in meters. This support allows axial movement, most notably thermal expansion.
        /// </summary>
        public double SupportB { get; set; }

        public override ICollection<Load> GetReactions()
        {
            List<Load> loadsForReactions = [];

            foreach (Load load in Loads)
            {
                loadsForReactions.AddRange(load.GetSimplifiedLoads());
            }

            double reactionMA, reactionAY, reactionBY;
            reactionMA = loadsForReactions.Where(l => l is MomentLoad).Sum(l => -l.Value) + loadsForReactions.Where(l => l is ForceLoad).Sum(l => -l.Value * (l.Position - SupportA));
            reactionBY = reactionMA / (SupportB - SupportA);
            loadsForReactions.Add(new ForceLoad
            {
                Value = reactionBY,
                Position = SupportB
            });
            reactionAY = loadsForReactions.Where(l => l is ForceLoad).Sum(l => -l.Value);

            return
            [
                new ForceLoad { Position = SupportA, Value = reactionAY },
                new ForceLoad { Position = SupportB, Value = reactionBY }
            ];
        }
    }
}

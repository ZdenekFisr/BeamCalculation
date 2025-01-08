namespace Application.Input
{
    public class OneSupportBeam : Beam
    {
        /// <summary>
        /// The position of the support from the left edge of the beam in meters. This support fixates the beam in all directions.
        /// </summary>
        public override double SupportA { get; set; }

        public override ICollection<Load> GetReactions()
        {
            List<Load> loadsForReactions = [];

            foreach (Load load in Loads)
            {
                loadsForReactions.AddRange(load.GetSimplifiedLoads());
            }

            double reactionMA, reactionAY;
            reactionMA = loadsForReactions.Where(l => l is MomentLoad).Sum(l => -l.Value) + loadsForReactions.Where(l => l is ForceLoad).Sum(l => -l.Value * l.Position);
            reactionAY = loadsForReactions.Where(l => l is ForceLoad).Sum(l => -l.Value);

            return
            [
                new MomentLoad { Position = SupportA, Value = reactionMA },
                new ForceLoad { Position = SupportA, Value = reactionAY }
            ];
        }
    }
}

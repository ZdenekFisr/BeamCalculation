using Application.Input;
using Application.PointComponents;

namespace Application.Services.AssignMoment
{
    /// <inheritdoc cref="IAssignMomentService"/>
    public class AssignMomentService : IAssignMomentService
    {
        /// <inheritdoc />
        public void AssignMoment(ICollection<IMoment> points, ICollection<Load> loads)
        {
            for (int i = 0; i < points.Count; i++)
            {
                IMoment point = points.ElementAt(i);

                double momentJump = 0;
                foreach (Load load in loads)
                {
                    point.Moment += load.GetInfluenceOnBendingMoment(point.Position);

                    if (load is MomentLoad momentLoad && momentLoad.Position == point.Position)
                    {
                        momentJump += momentLoad.Value;
                    }
                }

                if (momentJump != 0)
                    point.MomentJump = point.Moment + momentJump;
            }
        }
    }
}

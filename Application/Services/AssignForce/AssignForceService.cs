using Application.Input;
using Application.PointComponents;

namespace Application.Services.AssignForce
{
    /// <inheritdoc cref="IAssignForceService"/>
    public class AssignForceService : IAssignForceService
    {
        /// <inheritdoc />
        public void AssignForce(ICollection<IForce> points, ICollection<Load> loads)
        {
            for (int i = 0; i < points.Count; i++)
            {
                IForce point = points.ElementAt(i);

                double forceJump = 0;
                foreach (Load load in loads)
                {
                    point.Force += load.GetInfluenceOnShearForce(point.Position);

                    if (load is ForceLoad forceLoad && forceLoad.Position == point.Position)
                    {
                        forceJump += forceLoad.Value;
                    }
                }

                if (forceJump != 0)
                    point.ForceJump = point.Force - forceJump;
            }
        }
    }
}

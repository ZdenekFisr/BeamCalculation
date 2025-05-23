﻿using Application.Input;
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
                    (double force, double? forceBeforeJump) = load.GetInfluenceOnShearForce(point.Position);

                    point.Force += force;
                    if (forceBeforeJump.HasValue)
                        forceJump += forceBeforeJump.Value;
                }

                if (forceJump != 0)
                    point.ForceJump = point.Force - forceJump;
            }
        }
    }
}

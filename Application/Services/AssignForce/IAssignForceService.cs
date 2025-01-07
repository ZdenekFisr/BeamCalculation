using Application.Input;
using Application.PointComponents;

namespace Application.Services.AssignForce
{
    /// <summary>
    /// Service for assigning shear force to points.
    /// </summary>
    public interface IAssignForceService
    {
        /// <summary>
        /// Assigns shear force to points.
        /// </summary>
        /// <param name="points">Collection of points.</param>
        /// <param name="loads">Collection of loads.</param>
        void AssignForce(ICollection<IForce> points, ICollection<Load> loads);
    }
}
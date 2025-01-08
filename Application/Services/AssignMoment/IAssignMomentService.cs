using Application.Input;
using Application.PointComponents;

namespace Application.Services.AssignMoment
{
    /// <summary>
    /// Service for assigning bending moment to points.
    /// </summary>
    public interface IAssignMomentService
    {
        /// <summary>
        /// Assigns bending moment to points.
        /// </summary>
        /// <param name="points">Collection of points.</param>
        /// <param name="loads">Collection of loads.</param>
        void AssignMoment(ICollection<IMoment> points, ICollection<Load> loads);
    }
}
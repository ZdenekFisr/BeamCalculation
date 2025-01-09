using Application.Input;
using Application.PointComponents;

namespace Application.Services.AssignStress
{
    /// <summary>
    /// Service for assigning mechanical stress in bending to points.
    /// </summary>
    public interface IAssignStressService
    {
        /// <summary>
        /// Assigns mechanical stress in bending to the points.
        /// </summary>
        /// <param name="points">Collection of points.</param>
        /// <param name="moduli">Collection of section moduli.</param>
        /// <exception cref="InvalidOperationException">Thrown when the modulus of elasticity for a point is not defined correctly.</exception>
        void AssignStress(ICollection<IStress> points, ICollection<Modulus> moduli);
    }
}
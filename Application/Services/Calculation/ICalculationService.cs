using Application.Input;

namespace Application.Services.Calculation
{
    /// <summary>
    /// Service for calculating all parameters of the beam.
    /// </summary>
    public interface ICalculationService
    {
        /// <summary>
        /// Calculates all parameters of the beam.
        /// </summary>
        /// <param name="beam">The beam object.</param>
        /// <param name="lengthBetweenPoints">Space between each pair of points on the beam in meters.</param>
        void Calculate(Beam beam, double lengthBetweenPoints);
    }
}

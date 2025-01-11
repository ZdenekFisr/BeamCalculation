using Application.Input;

namespace Application.Services.Calculation
{
    /// <summary>
    /// Service for performing calculations on a beam.
    /// </summary>
    public interface ICalculationService
    {
        /// <summary>
        /// Calculates all parameters of the beam.
        /// </summary>
        /// <param name="beam">The beam object.</param>
        /// <param name="lengthBetweenPoints">Space between each pair of points on the beam in meters.</param>
        /// <returns>An <see cref="Output"/> object containing the calculated parameters of the beam.</returns>
        Output Calculate(Beam beam, double lengthBetweenPoints);
    }
}

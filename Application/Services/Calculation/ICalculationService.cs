using Application.Exceptions;
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
        /// <exception cref="InvalidModulusException">Thrown when at least one of the bending moduli is less than or equal to zero.</exception>
        /// <exception cref="LoadOutsideOfBeamException">Thrown when a load is placed outside of the beam.</exception>
        /// <exception cref="ContinuousLoadExceedsBeamException">Thrown when a continuous load exceeds the beam length.</exception>
        /// <exception cref="InvalidBeamLengthException">Thrown when the beam length is less than or equal to zero.</exception>
        /// <exception cref="InvalidLengthBetweenPointsException">Thrown when the length between points is less than or equal to zero.</exception>
        /// <exception cref="ModulusNotDefinedException">Thrown when the bending modulus for a point is not defined correctly.</exception>
        Output Calculate(Beam beam, double lengthBetweenPoints);
    }
}

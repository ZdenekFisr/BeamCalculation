using Application.Input;

namespace Application.Services.CreateLoads
{
    /// <summary>
    /// Service for creating loads.
    /// </summary>
    public interface ICreateLoadsService
    {
        /// <summary>
        /// Adds reaction loads to the original loads.
        /// </summary>
        /// <param name="originalLoads">Original loads.</param>
        /// <param name="reaction1">Reaction 1.</param>
        /// <param name="reaction2">Reaction 2.</param>
        /// <param name="beamLength">Length of the beam between supports or from one support to the end.</param>
        /// <param name="beamOverlapA">Overlap of the beam on the left side.</param>
        /// <returns>Collection of loads.</returns>
        Load[] CreateLoads(ICollection<Load> originalLoads, double reaction1, double reaction2, double beamLength, double beamOverlapA);
    }
}
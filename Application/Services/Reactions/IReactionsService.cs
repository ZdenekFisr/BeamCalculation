using Application.Input;

namespace Application.Services.Reactions
{
    public interface IReactionsService
    {
        /// <summary>
        /// Calculates the reactions at the supports of the beam.
        /// </summary>
        /// <param name="beam">The beam.</param>
        /// <returns>A tuple of two reactions: for one support it's vertical force and moment at the support and for two supports it's vertical forces in left (A) and right (B) support.</returns>
        /// <exception cref="NotImplementedException"></exception>
        (double reaction1, double reaction2) CalculateReactions(Beam beam);
    }
}

using Application.Input;

namespace Application.Services.CreateLoads
{
    /// <inheritdoc cref="ICreateLoadsService"/>
    public class CreateLoadsService : ICreateLoadsService
    {
        /// <inheritdoc />
        public Load[] CreateLoads(ICollection<Load> originalLoads, double reaction1, double reaction2, double beamLength, double beamOverlapA)
        {
            List<Load> loads = new(originalLoads);
            loads.AddRange([
                new ForceLoad
                {
                    Value = reaction1,
                    Position = beamOverlapA
                },
                new ForceLoad
                {
                    Value = reaction2,
                    Position = beamOverlapA + beamLength
                }]);

            return [.. loads];
        }
    }
}

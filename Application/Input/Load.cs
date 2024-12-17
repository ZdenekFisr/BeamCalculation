using Application.Enums;

namespace Application.Input
{
    /// <summary>
    /// Represents a load on the beam.
    /// </summary>
    public class Load
    {
        public double Value { get; set; }

        public LoadType Type { get; set; }

        public double Position { get; set; }

        public double Length { get; set; }
    }
}

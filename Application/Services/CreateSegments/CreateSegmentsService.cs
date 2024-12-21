using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CreateSegments
{
    /// <inheritdoc />
    public class CreateSegmentsService : ICreateSegmentsService
    {
        /// <inheritdoc />
        public Segment[] CreateSegments(double beamLength, double segmentLength)
        {
            int segmentsCount = (int)Math.Ceiling(Math.Round(beamLength / segmentLength, 10)) + 1;

            Segment[] segments = new Segment[segmentsCount];
            for (int i = 0; i < segmentsCount; i++)
            {
                segments[i] = new Segment
                {
                    Position = i * segmentLength
                };
            }

            return segments;
        }
    }
}

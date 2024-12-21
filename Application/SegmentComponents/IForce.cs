using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SegmentComponents
{
    public interface IForce
    {
        public double Position { get; set; }
        public double Force { get; set; }
        public double? ForceJump { get; set; }
    }
}

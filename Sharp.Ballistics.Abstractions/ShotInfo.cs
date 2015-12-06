using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Abstractions
{
    public class ShotInfo
    {
        public double BulletDrop { get; set; }

        public double WindDrift { get; set; }

        public double ElevationMOA { get; set; }

        public double ElevationClicks { get; set; }

        public double WindageMOA { get; set; }

        public double WindageClicks { get; set; }

        public double TimeToTargetSec { get; set; }

        public double ImpactVelocity { get; set; }

        public int Range { get; set; }
    }
}

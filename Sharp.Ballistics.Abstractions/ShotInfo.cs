using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace Sharp.Ballistics.Abstractions
{
    public class ShotInfo
    {
        public Length BulletDrop { get; set; }

        public Length WindDrift { get; set; }

        public double ElevationMOA { get; set; }

        public double ElevationClicks { get; set; }

        public double WindageMOA { get; set; }

        public double WindageClicks { get; set; }

        public double TimeToTargetSec { get; set; }

        public Speed ImpactVelocity { get; set; }

        public Length Range { get; set; }
    }
}

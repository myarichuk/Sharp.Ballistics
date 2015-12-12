using UnitsNet;

namespace Sharp.Ballistics.Abstractions
{
    public class BallisticSolution
    {
        public Length BulletDrop { get; set; }

        public Length WindDrift { get; set; }

        public Length SpinDrift { get; set; }

        public double VerticalMOA { get; set; }

        public double VerticalMils => VerticalMOA / 0.290888;

        public double VerticalClicks { get; set; }

        public double HorizontalMOA { get; set; }

        public double HorizontalMils => HorizontalMOA / 0.290888;

        public double HorizontalClicks { get; set; }

        public double TimeToTargetSec { get; set; }

        public Speed ImpactVelocity { get; set; }

        public Length Range { get; set; }

    }
}

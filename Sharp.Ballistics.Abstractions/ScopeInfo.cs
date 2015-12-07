using UnitsNet;

namespace Sharp.Ballistics.Abstractions
{
    public class ScopeInfo
    {
        public string Name { get; set; }

        public Length ZeroDistance { get; set; }

        public Length Height { get; set; }

        public double ElevationClicksPerMOA { get; set; }

        public double WindageClicksPerMOA { get; set; }
    }
}

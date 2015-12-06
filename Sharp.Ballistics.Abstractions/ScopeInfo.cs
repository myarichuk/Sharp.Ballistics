namespace Sharp.Ballistics.Abstractions
{
    public class ScopeInfo
    {
        public string Name { get; set; }

        public double ZeroDistance { get; set; }

        public double Height { get; set; }

        public double ElevationClicksPerMOA { get; set; }

        public double WindageClicksPerMOA { get; set; }
    }
}

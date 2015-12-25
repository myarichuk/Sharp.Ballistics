namespace Sharp.Ballistics
{
    public class Scope
    {
        public string Name { get; set; }

        public double ZeroDistance { get; set; }

        public double Height { get; set; }

        public double ElevationClicksPerMOA { get; set; }

        public double WindageClicksPerMOA { get; set; }

        public string Id { get; private set; }       
    }
}

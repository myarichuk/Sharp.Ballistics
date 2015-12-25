using System;
using UnitsNet;

namespace Sharp.Ballistics.Abstractions
{
    public class Scope : IHaveId, IEquatable<Scope>
    {
        public string Name { get; set; }

        public Length ZeroDistance { get; set; }

        public Length Height { get; set; }

        public double ElevationClicksPerMOA { get; set; }

        public double WindageClicksPerMOA { get; set; }

        public string Id { get; private set; }

        public bool Equals(Scope other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name, 
                            StringComparison.InvariantCultureIgnoreCase) && 
                ZeroDistance.Equals(other.ZeroDistance) && 
                Height.Equals(other.Height) && 
                ElevationClicksPerMOA.Equals(other.ElevationClicksPerMOA) && 
                WindageClicksPerMOA.Equals(other.WindageClicksPerMOA);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Scope) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Scope left, Scope right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Scope left, Scope right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"Name: {Name}, ZeroDistance: {ZeroDistance}, Height: {Height}, ElevationClicksPerMOA: {ElevationClicksPerMOA}, WindageClicksPerMOA: {WindageClicksPerMOA}";
        }
    }
}

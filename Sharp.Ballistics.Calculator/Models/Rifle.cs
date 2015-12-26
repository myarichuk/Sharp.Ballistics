using System;
using Sharp.Ballistics.Abstractions;
using UnitsNet;

namespace Sharp.Ballistics.Calculator.Models
{
    public class Rifle : IHaveId, IEquatable<Rifle>
    {
        public string Name { get; set; }        

        public Scope Scope { get; set; }

        public Cartridge Cartridge { get; set; }

        public WeatherCondition ZeroingWeather { get; set; }

        public Length BarrelTwist { get; set; }

        public string Id { get; private set; }

        public bool IsUsingNonListedScope { get; set; }

        public bool IsUsingNonListedAmmo { get; set; }

        public bool Equals(Rifle other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && 
                Equals(Scope, other.Scope) && 
                Equals(Cartridge, other.Cartridge) && 
                Equals(ZeroingWeather, other.ZeroingWeather) && 
                BarrelTwist.Equals(other.BarrelTwist);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Rifle) obj);
        }

        public override int GetHashCode()
        {
           return Id.GetHashCode();
        }

        public static bool operator ==(Rifle left, Rifle right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Rifle left, Rifle right)
        {
            return !Equals(left, right);
        }
    }
}

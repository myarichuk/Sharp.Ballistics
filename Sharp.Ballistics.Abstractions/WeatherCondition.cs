using System;
using UnitsNet;

namespace Sharp.Ballistics.Abstractions
{
    public class WeatherCondition : IEquatable<WeatherCondition>
    {
        //sea level -> 0 attitude, in feet
        public Length Altitude { get; set; }

        //The barometric pressure in inches of mercury(in Hg)
        //Standard pressure is 29.53 in Hg
        public Pressure Barometer { get; set; }

        //in Farenheight
        public Temperature Temperature { get; set; }
        
        //Ranges from 0.00 to 1.00, where 1.00 is 100% humidity
        public double RelativeHumidity { get; set; }

        private static WeatherCondition @default;
        public static WeatherCondition Default => @default ?? 
                                                  (@default = new WeatherCondition
                                                  {
                                                      Temperature = Temperature.FromDegreesCelsius(25),
                                                      Barometer = Pressure.FromPsi(14.503782),
                                                      RelativeHumidity = 0.5,
                                                      Altitude = Length.FromMeters(0)
                                                  });

        public bool Equals(WeatherCondition other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Altitude.Equals(other.Altitude) && 
                Barometer.Equals(other.Barometer) && 
                Temperature.Equals(other.Temperature) && 
                RelativeHumidity.Equals(other.RelativeHumidity);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((WeatherCondition) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Altitude.GetHashCode();
                hashCode = (hashCode*397) ^ Barometer.GetHashCode();
                hashCode = (hashCode*397) ^ Temperature.GetHashCode();
                hashCode = (hashCode*397) ^ RelativeHumidity.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(WeatherCondition left, WeatherCondition right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(WeatherCondition left, WeatherCondition right)
        {
            return !Equals(left, right);
        }
    }
}

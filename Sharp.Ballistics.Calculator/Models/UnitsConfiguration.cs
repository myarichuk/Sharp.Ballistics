using UnitsNet.Units;

namespace Sharp.Ballistics.Calculator.Models
{
    public class UnitsConfiguration
    {
        public LengthUnit Distance { get; set; }

        public LengthUnit ScopeHeight { get; set; }

        public SpeedUnit WindSpeed { get; set; }

        public SpeedUnit MuzzleSpeed { get; set; }

        public TemperatureUnit Temperature { get; set; }

        public PressureUnit Barometer { get; set; }

        public LengthUnit BulletOffsets { get; set; }

        public static UnitsConfiguration Metric => new UnitsConfiguration
        {
            Barometer = PressureUnit.Bar,
            BulletOffsets = LengthUnit.Centimeter,
            Distance = LengthUnit.Meter,
            MuzzleSpeed = SpeedUnit.MeterPerSecond,
            ScopeHeight = LengthUnit.Centimeter,
            Temperature = TemperatureUnit.DegreeCelsius,
            WindSpeed = SpeedUnit.KilometerPerHour
        };

        public static UnitsConfiguration Imperial => new UnitsConfiguration
        {
            Barometer = PressureUnit.Psi,
            BulletOffsets = LengthUnit.Inch,
            Distance = LengthUnit.Yard,
            MuzzleSpeed = SpeedUnit.FootPerSecond,
            ScopeHeight = LengthUnit.Inch,
            Temperature = TemperatureUnit.DegreeFahrenheit,
            WindSpeed = SpeedUnit.MilePerHour
        };
    }

}

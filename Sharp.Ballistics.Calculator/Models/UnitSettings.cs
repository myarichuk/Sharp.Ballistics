using UnitsNet.Units;
using Fasterflect;

namespace Sharp.Ballistics.Calculator.Models
{
    public class UnitSettings
    {
        public LengthUnit Distance { get; set; }

        public LengthUnit ScopeHeight { get; set; }

        public SpeedUnit WindSpeed { get; set; }

        public SpeedUnit MuzzleSpeed { get; set; }

        public TemperatureUnit Temperature { get; set; }

        public PressureUnit Barometer { get; set; }

        public LengthUnit BulletOffsets { get; set; }

        public LengthUnit Caliber { get; set; }

        public LengthUnit BarrelTwist { get; set; }

        public dynamic this[string unitTypeName] => this.GetPropertyValue(unitTypeName);

        public static UnitSettings Metric => new UnitSettings
        {
            Barometer = PressureUnit.Bar,
            BulletOffsets = LengthUnit.Centimeter,
            Distance = LengthUnit.Meter,
            MuzzleSpeed = SpeedUnit.MeterPerSecond,
            ScopeHeight = LengthUnit.Centimeter,
            Temperature = TemperatureUnit.DegreeCelsius,
            WindSpeed = SpeedUnit.KilometerPerHour,
            Caliber = LengthUnit.Millimeter,
            BarrelTwist = LengthUnit.Inch
        };

        public static UnitSettings Imperial => new UnitSettings
        {
            Barometer = PressureUnit.Psi,
            BulletOffsets = LengthUnit.Inch,
            Distance = LengthUnit.Yard,
            MuzzleSpeed = SpeedUnit.FootPerSecond,
            ScopeHeight = LengthUnit.Inch,
            Temperature = TemperatureUnit.DegreeFahrenheit,
            WindSpeed = SpeedUnit.MilePerHour,
            Caliber = LengthUnit.Inch,
            BarrelTwist = LengthUnit.Inch
        };
    }

}

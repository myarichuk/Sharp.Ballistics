using Sharp.Ballistics.Abstractions;

namespace Sharp.Ballistics.Calculator.Models
{
    public class CalculatorSettings
    {
        public Rifle CurrentRifle { get; set; }

        public Cartridge CurrentCartridge { get; set; }

        public WeatherCondition ShootingWeather { get; set; }
    }
}

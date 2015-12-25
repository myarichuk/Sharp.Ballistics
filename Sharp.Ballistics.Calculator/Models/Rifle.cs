using Sharp.Ballistics.Abstractions;
using UnitsNet;

namespace Sharp.Ballistics.Calculator.Models
{
    public class Rifle : IHaveId
    {
        public string Name { get; set; }        

        public Scope Scope { get; set; }

        public Cartridge Cartridge { get; set; }

        public WeatherCondition ZeroingWeather { get; set; }

        public Length BarrelTwist { get; set; }

        public string Id { get; private set; }       
    }
}

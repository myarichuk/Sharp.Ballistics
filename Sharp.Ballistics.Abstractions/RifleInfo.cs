using UnitsNet;

namespace Sharp.Ballistics.Abstractions
{
    public class RifleInfo
    {
        public string Name { get; set; }

        //1 twist per how many inches/mm -> for example, 1:12
        public Length BarrelTwist { get; set; }

        public WeatherCondition ZeroingConditions { get; set; }
    }
}

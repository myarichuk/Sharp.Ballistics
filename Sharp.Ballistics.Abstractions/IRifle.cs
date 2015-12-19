using System.Collections.Generic;
using UnitsNet;

namespace Sharp.Ballistics.Abstractions
{
    public interface IRifle : IHaveId
    {
        string Name { get; set; }

        WeatherCondition ZeroingConditions { get; set; }

        Cartridge Ammo { get; set; }

        Scope MountedScope { get; set; }

        BallisticSolution Solve(double shootingAngle, 
            Speed windSpeed, 
            double windAngle, 
            Length range, 
            WeatherCondition atmInfo,
            ShotLocationInfo shotLocationInfo);

        IEnumerable<BallisticSolution> SolveMultiple(double shootingAngle, 
            Speed windSpeed, 
            double windAngle, 
            IEnumerable<Length> range, 
            WeatherCondition atmInfo,
            ShotLocationInfo shotLocationInfo);
    }
}

using System.Collections.Generic;
using UnitsNet;

namespace Sharp.Ballistics.Abstractions
{
    public interface IRifle : IHaveId
    {
        string Name { get; set; }

        AtmosphericInfo ZeroingConditions { get; set; }

        AmmoInfo Ammo { get; set; }

        ScopeInfo Scope { get; set; }

        ShotInfo Solve(double shootingAngle, 
            Speed windSpeed, 
            double windAngle, 
            Length range, 
            AtmosphericInfo atmInfo);

        IEnumerable<ShotInfo> SolveMultiple(double shootingAngle, 
            Speed windSpeed, 
            double windAngle, 
            IEnumerable<Length> range, 
            AtmosphericInfo atmInfo);
    }
}

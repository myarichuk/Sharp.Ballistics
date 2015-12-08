using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace Sharp.Ballistics.Abstractions
{
    public interface IRifle
    {
        string Name { get; }

        AtmosphericInfo ZeroingConditions { get; }

        AmmoInfo Ammo { get; }

        ScopeInfo Scope { get; }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Abstractions
{
    public interface IRifle
    {        
        ShotInfo SolveShot(
            double shootingAngle, 
            double windSpeed,
            double windAngle,
            int range,
            AtmosphericInfo atmInfo);
    }
}

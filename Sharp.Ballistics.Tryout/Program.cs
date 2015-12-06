using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp.Ballistics.Abstractions;

namespace Sharp.Ballistics.Tryout
{
    class Program
    {
        static void Main(string[] args)
        {
            var rifleInfo = new RifleInfo
            {
                Name = "My Rifle",
                ZeroingConditions = new AtmosphericInfo
                {
                    Altitude = 0,
                    Barometer = 29.53, //inches of Mercury (Hg), standard sea level pressure
                    RelativeHumidity = 0.5, //in percentage from 0.0 to 1.0 (0% - 100%)
                    Temperature = 86 //farenheit -> 86F = 30C
                }
            };

            var ammoInfo = new AmmoInfo
            {
                MuzzleVelocity = 2580, // ft/sec
                BC = 0.505,
                DragFunction = DragFunction.G1,
                Name = "My Ammo"
            };

            var scopeInfo = new ScopeInfo
            {
                Name = "My Scope",
                Height = 1.5, //inches
                ZeroDistance = 109, //yards
                ElevationClicksPerMOA = 1,
                WindageClicksPerMOA = 0.5
            };

            var rifle = new Rifle(rifleInfo, scopeInfo, ammoInfo);

            var solution = rifle.SolveShot(
                0.0, //shooting angle
                3.1, //wind speed, miles/hour
                90, //wind direction angle (degrees)
                656, //shooting range -> yards
                null);
        }
    }
}

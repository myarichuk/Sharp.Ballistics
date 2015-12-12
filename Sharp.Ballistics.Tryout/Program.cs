using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp.Ballistics.Abstractions;
using UnitsNet;
using GNUBallisticsLibrary;

namespace Sharp.Ballistics.Tryout
{
    class Program
    {
        static void Main(string[] args)
        {
            var rifleInfo = new RifleInfo
            {
                Name = "My Rifle",
                BarrelTwist = Length.FromInches(11.25), //1:11.25
                ZeroingConditions = new WeatherCondition
                {
                    Altitude = Length.FromMeters(0), //sea level
                    Barometer = Pressure.FromPsi(14.7), //sea level
                    RelativeHumidity = 0.5, //in percentage from 0.0 to 1.0 (0% - 100%)
                    Temperature = Temperature.FromDegreesCelsius(30)
                }
            };

            var ammoInfo = new Ammunition
            {
                MuzzleVelocity = Speed.FromMetersPerSecond(790), 
                BC = 0.505,
                DragFunction = DragFunction.G1,
                Name = "My Ammo",
                WeightGrains = 175,
                Length = Length.FromInches(1.240),
                Caliber = Length.FromInches(0.308)
            };

            var scopeInfo = new Scope
            {
                Name = "My Scope",
                Height = Length.FromCentimeters(4), 
                ZeroDistance = Length.FromMeters(100), 
                ElevationClicksPerMOA = 1,
                WindageClicksPerMOA = 0.5
            };

            var locationInfo = new ShotLocationInfo
            {
                Latitude = 45, //degrees
                ShotAzimuth = 270 //degrees -> west
            };

            var currentWeatherConditions = new WeatherCondition
            {
                Altitude = Length.FromMeters(0), //sea level
                Barometer = Pressure.FromPsi(14.7), //sea level
                RelativeHumidity = 0.5, //in percentage from 0.0 to 1.0 (0% - 100%)
                Temperature = Temperature.FromDegreesCelsius(30)
            };

            var rifle = new Rifle(rifleInfo, scopeInfo, ammoInfo);

            var solution = rifle.Solve(
                0.0, //shooting angle
                Speed.FromKilometersPerHour(0),
                90, //wind direction angle (degrees)
                Length.FromMeters(500),
                null, 
                null);
        }
    }
}

# Sharp.Ballistics
This repository is essentially a .Net port of an excellent GNU Ballistics Library, which can be found here : <br/> http://sourceforge.net/projects/ballisticslib/
<br/>

So, how to use this? 
```c#
            var rifleInfo = new RifleInfo
            {
                Name = "My Rifle",
                ZeroingConditions = new AtmosphericInfo
                {
                    Altitude = Length.FromMeters(0), //sea level
                    Barometer = Pressure.FromPsi(14.7), //sea level
                    RelativeHumidity = 0.5, //in percentage from 0.0 to 1.0 (0% - 100%)
                    Temperature = Temperature.FromDegreesCelsius(30)
                }
            };

            var ammoInfo = new AmmoInfo
            {
                MuzzleVelocity = Speed.FromMetersPerSecond(750), 
                BC = 0.5,
                DragFunction = DragFunction.G1,
                Name = "My Ammo"
            };

            var scopeInfo = new ScopeInfo
            {
                Name = "My Scope",
                Height = Length.FromCentimeters(5), 
                ZeroDistance = Length.FromMeters(150), 
                ElevationClicksPerMOA = 1,
                WindageClicksPerMOA = 1
            };

            var rifle = new Rifle(rifleInfo, scopeInfo, ammoInfo);

            var solution = rifle.SolveShot(
                0.0, //shooting angle
                Speed.FromKilometersPerHour(5), 
                90, //wind direction angle (degrees)
                Length.FromMeters(350), 
                null);
```

Where ShotInfo is defined as
```c#
 public class ShotInfo
    {
        public Length BulletDrop { get; set; }

        public Length WindDrift { get; set; }

        public double ElevationMOA { get; set; }

        public double ElevationClicks { get; set; }

        public double WindageMOA { get; set; }

        public double WindageClicks { get; set; }

        public double TimeToTargetSec { get; set; }

        public Speed ImpactVelocity { get; set; }

        public Length Range { get; set; }
    }
```

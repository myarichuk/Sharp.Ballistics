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
                    Altitude = 0,
                    Barometer = 29.53, //inches of Mercury (Hg), standard sea level pressure
                    RelativeHumidity = 0.5, //in percentage from 0.0 to 1.0 (0% - 100%)
                    Temperature = 86 //farenheit -> 86F = 30C
                }
            };

            var ammoInfo = new AmmoInfo
            {
                MuzzleVelocity = 2500, // ft/sec
                BC = 0.5, 
                DragFunction = DragFunction.G1,
                Name = "My Ammo"
            };

            var scopeInfo = new ScopeInfo
            {
                Name = "My Scope",
                Height = 2, //inches
                ZeroDistance = 100, //yards
                ElevationClicksPerMOA = 1,
                WindageClicksPerMOA = 1
            };
            
            var rifle = new Rifle(rifleInfo, scopeInfo, ammoInfo);

            var solution = rifle.SolveShot(
                0.0, //shooting angle
                3, //wind speed, miles/hour
                90, //wind direction angle (degrees)
                328, //shooting range -> yards
                null);
```

Where ShotInfo is defined as
```c#
 public class ShotInfo
    {
        public double ElevationMOA { get; set; }

        public double ElevationClicks { get; set; }

        public double WindageMOA { get; set; }

        public double WindageClicks { get; set; }

        public double TimeToTargetSec { get; set; }

        public double ImpactVelocity { get; set; }

        public int Range { get; set; }
    }
```

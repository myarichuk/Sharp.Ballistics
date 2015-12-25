using UnitsNet;

namespace Sharp.Ballistics.Abstractions
{
    public class WeatherCondition
    {
        //sea level -> 0 attitude, in feet
        public Length Altitude { get; set; }

        //The barometric pressure in inches of mercury(in Hg)
        //Standard pressure is 29.53 in Hg
        public Pressure Barometer { get; set; }

        //in Farenheight
        public Temperature Temperature { get; set; }
        
        //Ranges from 0.00 to 1.00, where 1.00 is 100% humidity
        public double RelativeHumidity { get; set; }

        private static WeatherCondition @default;
        public static WeatherCondition Default => @default ?? 
                                                  (@default = new WeatherCondition
                                                  {
                                                      Temperature = Temperature.FromDegreesCelsius(25),
                                                      Barometer = Pressure.FromPsi(14.503782),
                                                      RelativeHumidity = 0.5,
                                                      Altitude = Length.FromMeters(0)
                                                  });
    }
}

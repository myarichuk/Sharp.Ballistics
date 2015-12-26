namespace Sharp.Ballistics
{
    public class WeatherCondition
    {
        //sea level -> 0 attitude, in feet
        public double Altitude { get; set; }

        //The barometric pressure in inches of mercury(in Hg)
        //Standard pressure is 29.53 in Hg
        public double Barometer { get; set; }

        //in Farenheight
        public double Temperature { get; set; }
        
        //Ranges from 0.00 to 1.00, where 1.00 is 100% humidity
        public double RelativeHumidity { get; set; }
    }
}

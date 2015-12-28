using UnitsNet;

namespace Sharp.Ballistics.Calculator.Models
{
    public class ShotInfo
    {
        public Length Range { get; set; }
        public double Angle { get; set; }
        public double WindDirection { get; set; }
        public Speed WindSpeed { get; set; }
    }
}

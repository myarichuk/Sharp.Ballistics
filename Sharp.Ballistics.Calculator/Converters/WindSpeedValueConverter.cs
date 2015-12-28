using UnitsNet;
using UnitsNet.Units;

namespace Sharp.Ballistics.Calculator
{
    public class WindSpeedValueConverter : FieldValueConverter<SpeedUnit, Speed>
    {
        protected override SpeedUnit GetRelevantUnitType()
        {
            return Units.WindSpeed;
        }
    }
}

using UnitsNet;
using UnitsNet.Units;

namespace Sharp.Ballistics.Calculator
{
    public class TargetSpeedValueConverter : FieldValueConverter<SpeedUnit, Speed>
    {
        protected override SpeedUnit GetRelevantUnitType()
        {
            return Units.TargetSpeed;
        }
    }
}

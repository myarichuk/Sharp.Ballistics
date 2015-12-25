using UnitsNet;
using UnitsNet.Units;

namespace Sharp.Ballistics.Calculator
{
    public class PressureValueConverter : FieldValueConverter<PressureUnit, Pressure>
    {
        protected override PressureUnit GetRelevantUnitType()
        {
            return Units.Barometer;
        }
    }
}

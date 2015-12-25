using UnitsNet;
using UnitsNet.Units;

namespace Sharp.Ballistics.Calculator
{
    public class TemperatureValueConverter : FieldValueConverter<TemperatureUnit, Temperature>
    {
        protected override TemperatureUnit GetRelevantUnitType()
        {
            return Units.Temperature;
        }
    }
}

using UnitsNet;
using UnitsNet.Units;

namespace Sharp.Ballistics.Calculator
{
    public class DistanceValueConverter : FieldValueConverter<LengthUnit, Length>
    {        
        protected override LengthUnit GetRelevantUnitType()
        {
            return Units.Distance;
        }
    }
}

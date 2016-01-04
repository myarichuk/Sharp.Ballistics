using UnitsNet;
using UnitsNet.Units;

namespace Sharp.Ballistics.Calculator
{
    public class BulletOffsetsValueConverter : FieldValueConverter<LengthUnit, Length>
    {        
        protected override LengthUnit GetRelevantUnitType()
        {
            return Units.BulletOffsets;
        }
    }
}

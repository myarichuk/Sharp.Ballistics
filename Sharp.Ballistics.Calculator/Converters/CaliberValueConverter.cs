using Sharp.Ballistics.Calculator.Bootstrap;
using Sharp.Ballistics.Calculator.Converters;
using Sharp.Ballistics.Calculator.Models;
using UnitsNet;
using UnitsNet.Units;

namespace Sharp.Ballistics.Calculator
{
    public class CaliberValueConverter : FieldValueConverter<LengthUnit, Length>
    {
        public CaliberValueConverter() :
            //I know, service locator is anti-pattern, couldn't think of something better here
            //For any suggestions, please don't hesitate to contact me :)
            base(AppBootstrapper.Container?.Resolve<ConfigurationModel>())
        {
        }

        protected override LengthUnit GetRelevantUnitType()
        {
            return Units.Caliber;
        }
    }
}

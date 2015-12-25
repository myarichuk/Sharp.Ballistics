using System;
using System.Globalization;
using System.Windows.Data;
using Humanizer;

namespace Sharp.Ballistics.Calculator
{
    public class EnumPluralizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (!(value.GetType().IsEnum))
                return value;

            return Enum.GetName(value.GetType(), value).Humanize().Pluralize();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (!(value.GetType().IsEnum))
                return value;

            return Enum.Parse(targetType,value.ToString().Singularize());
        }
    }
}

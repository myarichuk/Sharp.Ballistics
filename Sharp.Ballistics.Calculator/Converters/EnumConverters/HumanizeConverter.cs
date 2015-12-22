using System;
using System.Globalization;
using System.Windows.Data;
using UnitsNet;

namespace Sharp.Ballistics.Calculator
{
    public class HumanizeEnumConverter<TUnit> : IValueConverter
                    where TUnit : struct, IComparable, IFormattable
            {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return UnitSystem.GetDefaultAbbreviation((TUnit)value, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return UnitSystem.Parse<TUnit>((string)value, culture);
        }
    }
}

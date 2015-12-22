using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Sharp.Ballistics.Calculator
{
    public class RemoveUndefinedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enumArray = (IEnumerable)value;
            var returnArray = new List<object>();
            foreach (var val in enumArray)
            {
                if (!val.ToString().Contains("Undefined"))
                    returnArray.Add(val);
            }

            return returnArray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

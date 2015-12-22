using Dynamitey;
using Sharp.Ballistics.Calculator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using UnitsNet;

namespace Sharp.Ballistics.Calculator.Converters
{
    public abstract class FieldValueConverter<UnitType,UnitValueType> : IValueConverter
        where UnitType : struct, IComparable, IFormattable
    {
        protected UnitsConfiguration Units => configurationModel.Units;
        private readonly ConfigurationModel configurationModel;
        private static DependencyObject designModeIndicator = new DependencyObject();

        protected FieldValueConverter(ConfigurationModel configurationModel)
        {
            this.configurationModel = configurationModel;
            this.configurationModel?.Initialize();
        }

        protected abstract UnitType GetRelevantUnitType();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            dynamic type = (UnitValueType)value;
            var typeAsString = type.As(GetRelevantUnitType());

            return typeAsString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var numeric = (double)value;
            var staticContext = InvokeContext.CreateStatic;
            
            return Dynamic.InvokeMember(staticContext(typeof(UnitValueType)),
                        "From", GetRelevantUnitType());
        }
    }
}

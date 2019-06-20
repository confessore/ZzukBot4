using System;
using System.Globalization;
using System.Windows.Data;

namespace ZzukBot.GUI.Utilities.Converters
{
    internal class BoolInverterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var myBool = (bool) value;
            return !myBool;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace PayrollSystem.UI.Converters
{
    public class KeyEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is KeyEventArgs e)
            {
                return e;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

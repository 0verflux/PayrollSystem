using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PayrollSystem.UI.Converters
{
    public class DetailedSalaryAdjustmentInfoConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string code = values[0] as string;
            string desc = values[1] as string;

            return code + (!string.IsNullOrWhiteSpace(desc) ? $" ({desc})" : null);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

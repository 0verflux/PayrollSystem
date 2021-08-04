using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PayrollSystem.UI.Converters
{
    public class PrintDateRangeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var startDate = (DateTime)values[0];
            var endDate = (DateTime?)values[1];

            string str = null;
            str += startDate.ToString("d", culture.DateTimeFormat);

            if(endDate != null)
            {
                str += " - ";
                str += endDate?.ToString("d", culture.DateTimeFormat);
            }

            return str;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

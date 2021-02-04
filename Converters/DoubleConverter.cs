using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AddInCalculator2._0.Converters
{
    /// <summary>
    /// Converts a string to a double
    /// </summary>
    public class DoubleConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Double.TryParse((string)value, out Double result);
            return result;
        }
    }
}

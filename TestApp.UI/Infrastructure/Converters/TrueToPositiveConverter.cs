using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace TestApp.UI.Infrastructure.Converters
{
    [ValueConversion(typeof(bool), typeof(int))]
    public class TrueToPositiveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = value != null && (bool) value;
            if (boolValue)
            {
                return (int?) parameter ?? 1;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

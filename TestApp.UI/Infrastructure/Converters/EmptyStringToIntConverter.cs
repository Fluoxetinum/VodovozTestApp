using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace TestApp.UI.Infrastructure.Converters
{
    public class EmptyStringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            if (string.IsNullOrEmpty(strValue))
            {
                return -1;
            }

            try
            {
               return Int32.Parse(strValue);
            }
            catch (FormatException e)
            {
                return -1;
            }
        }
    }
}

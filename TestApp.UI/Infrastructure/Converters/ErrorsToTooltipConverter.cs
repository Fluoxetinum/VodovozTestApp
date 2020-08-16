using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace TestApp.UI.Infrastructure.Converters
{
    public class ErrorsToTooltipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = (Dictionary<string, List<string>>) value;
            if (collection == null || collection.Count == 0) return null;
            var pair = collection.First();
            string field = pair.Key;
            var errors = pair.Value;
            if (errors == null || errors.Count == 0) return null;
            string error = errors.First();

            return $"{field} - {error}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

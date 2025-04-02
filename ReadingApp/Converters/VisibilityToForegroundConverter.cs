using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ReadingApp.Converters
{
    public class VisibilityToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool visibility && visibility)
            {
                return new SolidColorBrush(Colors.Black);
            }
            else
            {
                // 完全透明
                return new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 
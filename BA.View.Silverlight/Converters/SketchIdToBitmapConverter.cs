using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BA.View.Silverlight.Converters
{
    public class SketchIdToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                value = -1;
            }

            string url = App.Current.SketchPageUrl + value.ToString();
            return new BitmapImage(new Uri(url));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

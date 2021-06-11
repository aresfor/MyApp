using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MyApp.ViewModels.SongListViewModel;

namespace MyApp.Converters
{
    public class ContentTypeToInt : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (ContentType)value;
        }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

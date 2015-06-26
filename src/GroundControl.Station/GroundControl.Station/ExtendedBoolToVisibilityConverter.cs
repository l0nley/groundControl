using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GroundControl.Station
{
  public class ExtendedBoolToVisibilityConverter : IValueConverter
  {
    private readonly BooleanToVisibilityConverter _converter;

    public ExtendedBoolToVisibilityConverter()
    {
      _converter = new BooleanToVisibilityConverter();
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var result = (Visibility) _converter.Convert(value, targetType, parameter, culture);
      if(parameter == null)
      {
        return result;
      }
      return result == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var result = (bool)_converter.Convert(value, targetType, parameter, culture);
      if(parameter != null)
      {
        return !result;
      }

      return result;
    }
  }
}

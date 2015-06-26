using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace GroundControl.Station
{
  public class HealthStatusValueConverter : IValueConverter
  {
    private readonly Regex _regex = new Regex(@"\d+(.\d+)?");
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var matches = _regex.Matches($"{value}");
      if (matches.Count <= 0)
      {
        return 0;
      }

      decimal decVal;
      if (decimal.TryParse(matches[0].Value, out decVal))
      {
        return (int)decVal;
      }

      return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}

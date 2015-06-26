using System;
using System.Windows;

namespace GroundControl.Station.Classes
{
  public class HealthStatus : DependencyObject
  {
    public static DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(HealthStatus));
    public static DependencyProperty TimestampProperty = DependencyProperty.Register("Timestamp", typeof(DateTime?), typeof(HealthStatus));
    public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(HealthStatus));

    public string Name
    {
      get
      {
        return (string)GetValue(NameProperty);
      }

      set
      {
        SetValue(NameProperty, value);
      }
    }

    public DateTime? Timestamp
    {
      get
      {
        return (DateTime?)GetValue(TimestampProperty);
      }

      set
      {
        SetValue(TimestampProperty, value);
      }
    }

    public object Value
    {
      get
      {
        return GetValue(ValueProperty);
      }

      set
      {
        SetValue(ValueProperty, value);
      }
    }
  }
}

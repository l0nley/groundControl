using System.Windows;

namespace GroundControl.Station.Classes
{
  public class ConnectionDescription : DependencyObject
  {

    public static DependencyProperty HandlerNameProperty = DependencyProperty.Register("HandlerName", typeof(string), typeof(ConnectionDescription));
    public static DependencyProperty ConnectionNameProperty = DependencyProperty.Register("ConnectionName", typeof(string), typeof(ConnectionDescription));
    public static DependencyProperty UriProperty = DependencyProperty.Register("Uri", typeof(string), typeof(ConnectionDescription));
    public static DependencyProperty IsReadonlyProperty = DependencyProperty.Register("IsReadonly", typeof(bool?), typeof(ConnectionDescription));

    public string HandlerName
    {
      get
      {
        return (string)GetValue(HandlerNameProperty);
      }
      set
      {
        SetValue(HandlerNameProperty, value);
      }
    }

    public bool? IsReadonly
    {
      get
      {
        return (bool?)GetValue(IsReadonlyProperty);
      }
      set
      {
        SetValue(IsReadonlyProperty, value);
      }
    }

    public string ConnectionName
    {
      get
      {
        return (string)GetValue(ConnectionNameProperty);
      }
      set
      {
        SetValue(ConnectionNameProperty, value);
      }
    }

    public string Uri
    {
      get
      {
        return (string)GetValue(UriProperty);
      }
      set
      {
        SetValue(UriProperty, value);
      }
    }
  }
}
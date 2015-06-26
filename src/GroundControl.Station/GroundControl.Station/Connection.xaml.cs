using System.Windows;
using System.Windows.Controls;

namespace GroundControl.Station
{
  /// <summary>
  /// Interaction logic for Connection.xaml
  /// </summary>
  public partial class Connection : UserControl
  {
    public Connection()
    {
      InitializeComponent();
    }

    public static DependencyProperty HandlerNameProperty = DependencyProperty.Register("HandlerName", typeof(string), typeof(Connection));
    public static DependencyProperty ConnectionNameProperty = DependencyProperty.Register("ConnectionName", typeof(string), typeof(Connection));
    public static DependencyProperty UriProperty = DependencyProperty.Register("Uri", typeof(string), typeof(Connection));
    public static DependencyProperty IsReadonlyProperty = DependencyProperty.Register("IsReadonly", typeof(bool?), typeof(Connection));

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

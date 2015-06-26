using System.Windows;
using System.Windows.Media;

namespace GroundControl.Station.Classes
{
  public class BarItem : DependencyObject
  {

    public static DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(BarItem));

    public BarItem()
    {
      Color = new SolidColorBrush(Colors.DarkBlue);
    }

    public SolidColorBrush Color
    {
      get
      {
        return (SolidColorBrush)GetValue(ColorProperty);
      }

      set
      {
        SetValue(ColorProperty, value);
      }
    }
  }
}

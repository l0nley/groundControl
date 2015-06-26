using GroundControl.Station.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GroundControl.Station
{
  /// <summary>
  /// Interaction logic for Bar.xaml
  /// </summary>
  public partial class Bar : UserControl
  {

    public static readonly DependencyProperty BarNameProperty
      = DependencyProperty.Register("BarName", typeof(string), typeof(Bar));

    public static readonly DependencyProperty LevelProperty
      = DependencyProperty.Register("Level", typeof(int?), typeof(Bar), new PropertyMetadata(LevelChangedCallback));

    public static readonly DependencyProperty MaxProperty
      = DependencyProperty.Register("Max", typeof(int?), typeof(Bar));

    public static readonly DependencyProperty MinProperty
      = DependencyProperty.Register("Min", typeof(int?), typeof(Bar));

    public static void LevelChangedCallback(DependencyObject d,  DependencyPropertyChangedEventArgs e)
    {
      (d as Bar).LevelChanged();
    }


    public Bar()
    {
      SetValue(NameProperty, "BAR" + this.GetHashCode());
      Max = 100;
      Min = 0;
      Items = new List<BarItem>();
      for (var i = 0; i < 20; i++)
      {
        Items.Add(new BarItem());
      }

      InitializeComponent();
    }

    public List<BarItem> Items { get; set; }

    public string BarName
    {
      get
      {
        return GetValue(BarNameProperty) as string;
      }
      set
      {
        SetValue(BarNameProperty, value);
      }
    }

    public int? Level
    {
      get
      {
        return GetValue(LevelProperty) as int?;
      }
      set
      {
        SetValue(LevelProperty, value);
      }
    }

    public int? Max
    {
      get
      {
        return GetValue(MaxProperty) as int?;
      }

      set
      {
        SetValue(MaxProperty, value);
      }
    }

    public int? Min
    {
      get
      {
        return (int?)GetValue(MinProperty);
      }

      set
      {
        SetValue(MinProperty, value);
      }
    }

    private void LevelChanged()
    {
      if (Level * 1.3 > Max)
      {
        Max = (int)(Level * 1.3);
      }
      if (Level < Max * 0.2)
      {
        Max = (int)(Max * 0.8);
      }

      var maxColor = Math.Sqrt(Max.GetValueOrDefault());
      var greenBorder = 0.5 * maxColor;
      var yellowBorder = 0.8 * maxColor;
      var level = Math.Sqrt(Level.GetValueOrDefault());
      var step = maxColor / (double)Items.Count;
      var currentLevel = 0.0;
      foreach (var item in Items)
      {
        if (currentLevel < level)
        {
          if (currentLevel <= greenBorder)
          {
            item.Color = new SolidColorBrush(Colors.Green);
          }
          else
          if (currentLevel <= yellowBorder)
          {
            item.Color = new SolidColorBrush(Colors.Yellow);
          }
          else
          {
            item.Color = new SolidColorBrush(Colors.Red);
          }
        }
        else
        {
          item.Color = new SolidColorBrush(Colors.Black);
        }
        currentLevel += step;
      }
    }
  }
}

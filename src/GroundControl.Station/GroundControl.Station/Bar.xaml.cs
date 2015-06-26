using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace GroundControl.Station
{
  /// <summary>
  /// Interaction logic for Bar.xaml
  /// </summary>
  public partial class Bar : UserControl, INotifyPropertyChanged
  {

    private int _max;
    private int _min;
    private ObservableCollection<BarItem> _items;


    public static readonly DependencyProperty BarNameProperty
      = DependencyProperty.Register("BarName", typeof(string), typeof(Bar));

    public static readonly DependencyProperty LevelProperty
      = DependencyProperty.Register("Level", typeof(int?), typeof(Bar));

    public Bar()
    {
      SetValue(NameProperty, "BAR" + this.GetHashCode());
      _min = 0;
      _max = 100;
      _items = new ObservableCollection<BarItem>();
      for (var i = 0; i < 20; i++)
      {
        _items.Add(new BarItem());
      }

      InitializeComponent();
    }

    public ObservableCollection<BarItem> Items
    {
      get
      {
        return _items;
      }
      set
      {
        _items = value;
        OnPropertyChanged();
      }
    }

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
        LevelChanged();
      }
    }

    public int Max
    {
      get
      {
        return _max;
      }

      set
      {
        _max = value;
        OnPropertyChanged();
      }
    }

    public int Min
    {
      get
      {
        return _min;
      }

      set
      {
        _min = value;
        OnPropertyChanged();
      }
    }

    private void LevelChanged()
    {
      if (Level > Max)
      {
        Max = (int)(Level * 1.3);
      }
      if (Level < Max * 0.5)
      {
        Max = (int)(Max * 0.7);
      }

      var maxColor = Math.Sqrt(Max);
      var greenBorder = 0.33 * maxColor;
      var yellowBorder = 0.66 * maxColor;
      var level = Math.Sqrt(Level.GetValueOrDefault());
      var step = Max / (double)_items.Count;
      var currentLevel = 0.0;
      foreach (var item in _items)
      {
        var y = Math.Sqrt(currentLevel);
        if (level <= currentLevel)
        {
          if (y <= greenBorder)
          {
            item.Level = 3;
          }
          else
          if (y <= yellowBorder)
          {
            item.Level = 2;
          }
          else
          {
            item.Level = 1;
          }
        }
        else
        {
          item.Level = 0;
        }

        currentLevel += step;
      }
    }

    private void OnPropertyChanged([CallerMemberName]string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}

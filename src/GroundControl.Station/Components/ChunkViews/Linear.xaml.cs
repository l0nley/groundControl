using GroundControl.Station.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GroundControl.Station.Components.ChunkViews
{
  /// <summary>
  /// Interaction logic for Linear.xaml
  /// </summary>
  public partial class Linear : UserControl, INotifyPropertyChanged
  {
    private double _max;
    private double _min;
    private Dispatcher _dispatcher;
    private List<double> _list;
    private Point[] _points;

    public Linear()
    {
      _dispatcher = Dispatcher.CurrentDispatcher;
      _list = new List<double>();
      InitializeComponent();
      Max = double.MinValue;
      Min = double.MaxValue;
    }

    private ChunkViewModel Model
    {
      get
      {
        return DataContext as ChunkViewModel;
      }
    }

    public double Min
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

    public double Max
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

    private void OnPropertyChanged([CallerMemberName]string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
      var model = Model;
      if (model == null)
      {
        return;
      }
      Model.PropertyChanged += Model_PropertyChanged;
    }

    private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      try
      {
        if (e.PropertyName != "Value")
        {
          return;
        }

        var val = _dispatcher.Invoke(() => ExtractValue(Model.Value));


        double[] copy;
        
        lock (_list)
        {
          _list.Add(val);

          while (_list.Count > 10)
          {
            _list.RemoveAt(0);
          }
          copy = _list.ToArray();
        }

        var max = _max = copy.Max() * 1.05;
        _dispatcher.Invoke(() => OnPropertyChanged("Max"));
        var min = _min = copy.Min() * 0.95;
        _dispatcher.Invoke(() => OnPropertyChanged("Min"));

        var height = Math.Abs(max - min);
        var mashy = _dispatcher.Invoke(() => Canvas.ActualHeight) / height;
        var mashx = _dispatcher.Invoke(() => Canvas.ActualWidth) / 10.0;
        var lstPoints = new List<Point>();
        for (var i = 0; i < copy.Length; i++)
        {
          lstPoints.Add(new Point
          {
            X = i * mashx,
            Y = (copy[i] - min) * mashy
          });
        }
        _dispatcher.Invoke(() => Points = lstPoints.ToArray());
      }
      catch (Exception)
      {
        // ignored
      }
    }

    public Point[] Points
    {
      get
      {
        return _points;
      }

      set
      {
        _points = value;
        OnPropertyChanged();
      }
    }

    private double ExtractValue(byte[] value)
    {
      return BitConverter.ToSingle(value, 0);
    }
  }
}

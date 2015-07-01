using GroundControl.Station.Components;
using GroundControl.Station.ViewModels;
using Microsoft.VisualBasic;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows;

namespace GroundControl.Station
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      Model = new MainWindowViewModel();
      InitializeComponent();
    }

    public MainWindowViewModel Model { get; set; }

    private void ComponentsDataAggregator(object sender, RoutedEventArgs e)
    {
      var agg = new DataAggregatorViewModel
      {
        Chunks = new ConcurrentQueue<Core.IChunk>()
      };
      var temp = "AG" + agg.GetHashCode();
      temp = Interaction.InputBox("Data aggregator name", Title, temp);
      agg.Name = temp;
      var dtg = new DataAggregator
      {
        DataContext = agg
      };
      ElementsPanel.Children.Add(dtg);
      Model.DataAggregators.Add(agg);
    }

    private void Window_Closed(object sender, System.EventArgs e)
    {
      foreach(var child in ElementsPanel.Children)
      {
        var cleanup = child as ICleanup;
        if(cleanup!=null)
        {
          cleanup.Cleanup();
        }
      }
    }

    private void MoveControl(object sender, RoutedEventArgs e)
    {
      var items = new List<DropDownItem>();
      foreach(var item in ElementsPanel.Children)
      {
        var ag = item as DataAggregator;
        if(ag != null)
        {
          items.Add(new DropDownItem
          {
            DisplayText = ag.Model.Name,
            Value = ag.Model
          });
        }
      }

      var win = new DropDownWindow
      {
        Text = "Choose harvester to connect"
      };

      foreach(var item in items)
      {
        win.Items.Add(item);
      }

      if(win.ShowDialog() != true)
      {
        return;
      }

      var value = win.SelectedItem;
      if(value == null)
      {
        return;
      }
      var agg = (DataAggregatorViewModel)value.Value;

      var moveControl = new MoveControl
      {
        DataContext = new MoveControlViewModel
        {
          Harvester = agg,
          ConnectedTo = agg.Name
        }
      };

      ElementsPanel.Children.Add(moveControl);
    }
  }
}

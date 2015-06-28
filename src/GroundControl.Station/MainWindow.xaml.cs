using GroundControl.Station.Components;
using GroundControl.Station.ViewModels;
using Microsoft.VisualBasic;
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
      var agg = new DataAggregatorViewModel();
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
  }
}

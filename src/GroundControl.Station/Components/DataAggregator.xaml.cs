using GroundControl.Station.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace GroundControl.Station.Components
{
  /// <summary>
  /// Interaction logic for DataAggregator.xaml
  /// </summary>
  public partial class DataAggregator : UserControl, ICleanup
  {
    public DataAggregator()
    {
      InitializeComponent();
    }

    private DataAggregatorViewModel Model
    {
      get
      {
        return (DataAggregatorViewModel)DataContext;
      }
    }

    public void Cleanup()
    {
      if (Model.Online)
      {
        Model.Online = false;
      }
    }

    private void AddConnection(object sender, RoutedEventArgs e)
    {
      var editor = new ConnectionEditor();
      if (editor.ShowDialog().GetValueOrDefault() == false)
      {
        return;
      }

      var con = new ConnectionViewModel
      {
        Name = editor.ConnectionName,
        Uri = editor.Uri
      };

      Model.Connections.Add(con);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      Model.Online = !Model.Online;
    }
  }
}

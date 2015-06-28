using GroundControl.Station.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace GroundControl.Station.Components
{
  /// <summary>
  /// Interaction logic for DataAggregator.xaml
  /// </summary>
  public partial class DataAggregator : UserControl
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


    private void AddConnection(object sender, RoutedEventArgs e)
    {
      var editor = new ConnectionEditor();
      if (editor.ShowDialog().GetValueOrDefault() == false)
      {
        return;
      }

      var con = new ConnectionViewModel
      {
        FullTypeName = editor.SelectedItem.Value.FullName,
        Name = editor.ConnectionName,
        TypeName = editor.SelectedItem.Value.Name,
        Uri = editor.Uri
      };

      Model.Connections.Add(con);
    }
  }
}

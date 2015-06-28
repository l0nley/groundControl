using GroundControl.Station.ViewModels;
using System.Windows.Controls;

namespace GroundControl.Station.Components
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

    public ConnectionViewModel Model
    {
      get
      {
        return DataContext as ConnectionViewModel;
      }
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
     if(Model == null)
      {
        return;
      }

      Model.NotCollapsed = !Model.NotCollapsed.GetValueOrDefault();
    }

  }
}

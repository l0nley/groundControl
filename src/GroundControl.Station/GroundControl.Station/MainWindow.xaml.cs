using System.Windows;
using System.Windows.Input;

namespace GroundControl.Station
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void NewProject(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
    {

    }

    private void Exit(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }

    public void DragWindow(object sender, MouseButtonEventArgs args)
    {
      DragMove();
    }
  }
}

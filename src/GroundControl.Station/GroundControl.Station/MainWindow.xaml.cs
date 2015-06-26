using GroundControl.Station.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace GroundControl.Station
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window, INotifyPropertyChanged
  {
    private Project _project;

    public MainWindow()
    {
      InitializeComponent();
    }

    public Project Project
    {
      get
      {
        return _project;
      }

      set
      {
        _project = value;
        OnPropertyChanged();
      }
    }



    private void OnPropertyChanged([CallerMemberName]string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;

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

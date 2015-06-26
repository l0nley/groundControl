using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GroundControl.Station.Classes;
using System.Runtime.CompilerServices;

namespace GroundControl.Station
{
  /// <summary>
  /// Interaction logic for DataHarvesterView.xaml
  /// </summary>
  public partial class DataHarvesterView : UserControl, INotifyPropertyChanged
  {
    ObservableCollection<HealthStatus> _health;
    public DataHarvesterView()
    {
      InitializeComponent();
      Health = new ObservableCollection<HealthStatus>();
      Health.Add(new HealthStatus
      {
        Name = "Frames/sec",
        Value = 20
      });
      Health.Add(new HealthStatus
      {
        Name = "Control/sec",
        Value = 50
      });
    }

    public ObservableCollection<HealthStatus> Health
    {
      get
      {
        return _health;
      }
      set
      {
        _health = value;
        OnPropertyChanged();
      }
    }

    private void OnPropertyChanged([CallerMemberName]string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void Switcher_SwitcherTriggered(object sender, System.EventArgs e)
    {
      MessageBox.Show("Ope!");
    }
  }
}

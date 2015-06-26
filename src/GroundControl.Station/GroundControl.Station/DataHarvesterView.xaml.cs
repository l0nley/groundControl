using System.Collections.ObjectModel;
using System.Windows.Controls;
using GroundControl.Station.Classes;
using System.Windows;
using GroundControl.Connections;

namespace GroundControl.Station
{
  /// <summary>
  /// Interaction logic for DataHarvesterView.xaml
  /// </summary>
  public partial class DataHarvesterView : UserControl
  {

    public static DependencyProperty IsRunningProperty = DependencyProperty.Register("IsRunning", typeof(bool?), typeof(DataHarvesterView));
    public static DependencyProperty HarvesterNameProperty = DependencyProperty.Register("HarvesterName", typeof(string), typeof(DataHarvesterView));

    public DataHarvesterView()
    {
      InitializeComponent();
      HarvesterName = "Buzzer Harvester" + this.GetHashCode();
      Health = new ObservableCollection<HealthStatus>
      {
        new HealthStatus
        {
          Name = "Frames/sec",
          Value = 20
        },
        new HealthStatus
        {
          Name = "Control/sec",
          Value = 50
        }
      };

      Connections = new ObservableCollection<ConnectionDescription>
      {
        new ConnectionDescription
        {
          ConnectionName = "Buzzer#1",
          HandlerName = typeof(BuzzerConnectionHandler).Name,
          Uri = "device://buzzer1"
        },
        new ConnectionDescription
        {
          ConnectionName = "Buzzer#1",
          HandlerName = typeof(BuzzerConnectionHandler).Name,
          Uri = "device://buzzer2"
        }
      };
    }

    public ObservableCollection<HealthStatus> Health { get; set; }

    public ObservableCollection<ConnectionDescription> Connections { get; set; }

    public string HarvesterName
    {
      get
      {
        return (string)GetValue(HarvesterNameProperty);
      }
      set
      {
        SetValue(HarvesterNameProperty, value);
      }
    }

    public bool? IsRunning
    {
      get
      {
        return (bool?)GetValue(IsRunningProperty);
      }
      set
      {
        SetValue(IsRunningProperty, value);
      }
    }

    private void Switcher_SwitcherTriggered(object sender, System.EventArgs e)
    {
      IsRunning = !IsRunning;
    }
  }
}

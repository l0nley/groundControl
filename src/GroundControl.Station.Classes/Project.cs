using System.Collections.ObjectModel;

namespace GroundControl.Station.Classes
{
  public class Project : GroupContainer
  {
    private bool _startHealthMonitoring;
    private string _projectName;
    private ObservableCollection<ConnectionDescription> _connectionDescriptions;

    public Project()
    {
      StartHealthMonitoring = true;
      ConnectionDescriptions = new ObservableCollection<ConnectionDescription>();
      _projectName = "New Telemetry Project";
    }

    public string ProjectName
    {
      get { return _projectName; }
      set {
        if (value == _projectName) return;
        _projectName = value;
        OnPropertyChanged();
      }
    }

    public bool StartHealthMonitoring
    {
      get { return _startHealthMonitoring; }
      set
      {
        if (value == _startHealthMonitoring) return;
        _startHealthMonitoring = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<ConnectionDescription> ConnectionDescriptions
    {
      get { return _connectionDescriptions; }
      set
      {
        if (Equals(value, _connectionDescriptions)) return;
        _connectionDescriptions = value;
        OnPropertyChanged();
      }
    }
  }
}

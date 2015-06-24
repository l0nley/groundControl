using System.Collections.ObjectModel;

namespace GroundControl.Station.Classes
{
  public class Project : GroupContainer
  {
    private bool _startHealthMonitoring;
    private ObservableCollection<ConnectionDescription> _connectionDescriptions;

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

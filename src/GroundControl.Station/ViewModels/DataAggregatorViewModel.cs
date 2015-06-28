using System.Collections.ObjectModel;

namespace GroundControl.Station.ViewModels
{
  public class DataAggregatorViewModel : ViewModelBase
  {
    private string _name;
    private readonly ObservableCollection<ConnectionViewModel> _connections;
    private readonly ObservableCollection<HealthItemViewModel> _health;

    public DataAggregatorViewModel()
    {
      _connections = new ObservableCollection<ConnectionViewModel>();
      _health = new ObservableCollection<HealthItemViewModel>();
    }

    public string Name
    {
      get
      {
        return _name;
      }

      set
      {
        _name = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<ConnectionViewModel> Connections
    {
      get
      {
        return _connections;
      }
    }

    public ObservableCollection<HealthItemViewModel> Health
    {
      get
      {
        return _health;
      }
    }
  }
}

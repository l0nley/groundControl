using GroundControl.Station.Components;

namespace GroundControl.Station.ViewModels
{
  public class MoveControlViewModel : ViewModelBase
  {
    private MoveType _moveType;

    private string _connectedTo;

    private DataAggregatorViewModel _model;

    public MoveType MoveType
    {
      get
      {
        return _moveType;
      }
      set
      {
        _moveType = value;
        OnPropertyChanged();
      }
    }

    public DataAggregatorViewModel Harvester
    {
      get
      {
        return _model;
      }
      set
      {
        _model = value;
        OnPropertyChanged();
      }
    }

    public string ConnectedTo
    {
      get
      {
        return _connectedTo;
      }
      set
      {
        _connectedTo = value;
        OnPropertyChanged();
      }
    }
  }
}

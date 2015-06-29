namespace GroundControl.Station.ViewModels
{
  public class ConnectionViewModel : ViewModelBase
  {
    private string _name;
    private string _uri;

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

    public string Uri
    {
      get
      {
        return _uri;
      }
      set
      {
        _uri = value;
        OnPropertyChanged();
      }
    }
  }
}

namespace GroundControl.Station.ViewModels
{
  public class ConnectionViewModel : ViewModelBase
  {
    private string _name;
    private string _typeName;
    private string _fullTypeName;
    private string _uri;
    private bool? _notCollapsed;

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

    public bool? NotCollapsed
    {
      get
      {
        return _notCollapsed;
      }

      set
      {
        _notCollapsed = value;
        OnPropertyChanged();
      }
    }

    public string TypeName
    {
      get
      {
        return _typeName;
      }
      set
      {
        _typeName = value;
        OnPropertyChanged();
      }
    }

    public string FullTypeName
    {
      get
      {
        return _fullTypeName;
      }

      set
      {
        _fullTypeName = value;
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

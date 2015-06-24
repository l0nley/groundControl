namespace GroundControl.Station.Classes
{
  public class TelemetryInformation : LayoutElement
  {
    private string _typeFullName;
    private string _description;
    private string _shortName;
    private DisplayType _displayType;
    private AdditionalInformation _additionalInformation;

    public string TypeFullName
    {
      get { return _typeFullName; }
      set
      {
        if (value == _typeFullName) return;
        _typeFullName = value;
        OnPropertyChanged();
      }
    }

    public string Description
    {
      get { return _description; }
      set
      {
        if (value == _description) return;
        _description = value;
        OnPropertyChanged();
      }
    }

    public string ShortName
    {
      get { return _shortName; }
      set
      {
        if (value == _shortName) return;
        _shortName = value;
        OnPropertyChanged();
      }
    }

    public DisplayType DisplayType
    {
      get { return _displayType; }
      set
      {
        if (value == _displayType) return;
        _displayType = value;
        OnPropertyChanged();
      }
    }

    public AdditionalInformation AdditionalInformation
    {
      get { return _additionalInformation; }
      set
      {
        if (value == _additionalInformation) return;
        _additionalInformation = value;
        OnPropertyChanged();
      }
    }
  }
}
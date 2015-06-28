namespace GroundControl.Station.ViewModels
{
  public class HealthItemViewModel : ViewModelBase
  {
    private string _name;
    private object _value;

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

    public object Value
    {
      get
      {
        return _value;
      }
      set
      {
        _value = value;
        OnPropertyChanged();
        OnPropertyChanged("ViewValue");
      }
    }

    public string ViewValue
    {
      get
      {
        return Value?.ToString();
      }
    }
  }
}

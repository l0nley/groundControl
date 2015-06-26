using GroundControl.Core;
using GroundControl.Station.Classes.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GroundControl.Station.Classes
{
  public class HealthStatus : INotifyPropertyChanged, IHealthDescription
  {
    private string _name;
    private DateTime _timeStamp;
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

    public DateTime Timestamp
    {
      get
      {
        return _timeStamp;
      }

      set
      {
        _timeStamp = value;
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
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}

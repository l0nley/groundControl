using System.ComponentModel;
using System.Runtime.CompilerServices;
using GroundControl.Station.Classes.Annotations;

namespace GroundControl.Station.Classes
{
  public class ConnectionDescription : INotifyPropertyChanged
  {
    private string _handlerTypeFullName;

    public string HandlerTypeFullName
    {
      get { return _handlerTypeFullName; }
      set
      {
        if (value == _handlerTypeFullName) return;
        _handlerTypeFullName = value;
        OnPropertyChanged();
      }
    }

    public string Uri
    {
      get { return _uri; }
      set
      {
        if (value == _uri) return;
        _uri = value;
        OnPropertyChanged();
      }
    }

    private string _uri;
    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
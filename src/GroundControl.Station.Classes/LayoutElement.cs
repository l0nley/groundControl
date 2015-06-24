using System.ComponentModel;
using System.Runtime.CompilerServices;
using GroundControl.Station.Classes.Annotations;

namespace GroundControl.Station.Classes
{
  public abstract class LayoutElement : INotifyPropertyChanged
  {
    private int _width;

    public virtual int Width
    {
      get { return _width; }
      set
      {
        if (value == _width) return;  
        _width = value;
        OnPropertyChanged();
      }
    }

    public virtual int Height
    {
      get { return _height; }
      set
      {
        if (value == _height) return;
        _height = value;
        OnPropertyChanged();
      }
    }

    private int _height;

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
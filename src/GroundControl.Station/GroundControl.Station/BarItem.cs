using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GroundControl.Station
{
  public class BarItem : INotifyPropertyChanged
  {

    private int? _level;

    public int? Level
    {
      get
      {
        return _level;    
      }
      set
      {
        _level = value;
        OnPropertyChanged();
      }
    }

    private void OnPropertyChanged([CallerMemberName]string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}

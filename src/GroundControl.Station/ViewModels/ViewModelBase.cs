using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GroundControl.Station.ViewModels
{
  public abstract class ViewModelBase : INotifyPropertyChanged
  {
    protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public event PropertyChangedEventHandler PropertyChanged;
  }
}

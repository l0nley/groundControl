using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace GroundControl.Station
{
  /// <summary>
  /// Interaction logic for Switcher.xaml
  /// </summary>
  public partial class Switcher : UserControl, INotifyPropertyChanged
  {
    private bool _state;

    public Switcher()
    {
      InitializeComponent();
    }


    public bool State
    {
      get
      {
        return _state;
      }
      set
      {
        _state = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(State)));
      }
    }

    public event EventHandler SwitcherTriggered; 

    public event PropertyChangedEventHandler PropertyChanged;

    private void SwictherTriggered(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
    {
      State = !State;
      SwitcherTriggered?.Invoke(this, new EventArgs());
    }
  }
}

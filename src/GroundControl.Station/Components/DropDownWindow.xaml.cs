using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace GroundControl.Station.Components
{
  /// <summary>
  /// Interaction logic for DropDownWindow.xaml
  /// </summary>
  public partial class DropDownWindow : Window, INotifyPropertyChanged
  {
    private string _text;
    private ObservableCollection<DropDownItem> _items;
    private DropDownItem _selectedItem;

    public DropDownWindow()
    {
      _items = new ObservableCollection<DropDownItem>();
      InitializeComponent();
    }

    public string Text
    {
      get
      {
        return _text;
      }

      set
      {
        _text = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<DropDownItem> Items
    {
      get
      {
        return _items;
      }
    }

    public DropDownItem SelectedItem
    {
      get
      {
        return _selectedItem;
      }
      set
      {
        _selectedItem = value;
        OnPropertyChanged();
      }
    }

    private void OnPropertyChanged([CallerMemberName]string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = true;
      Close();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
      Close();
    }
  }
}

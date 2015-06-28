using GroundControl.Connections;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;

namespace GroundControl.Station.Components
{
  /// <summary>
  /// Interaction logic for ConnectionEditor.xaml
  /// </summary>
  public partial class ConnectionEditor : Window, INotifyPropertyChanged
  {
    private string _connectionName;
    private DropdownItem<Type> _selectedItem;
    private ObservableCollection<DropdownItem<Type>> _types;
    private string _uri;

    public ConnectionEditor()
    {
      _types = new ObservableCollection<DropdownItem<Type>>();
      var types = typeof(ConnectionHandlerBase)
        .Assembly
        .GetTypes()
        .Where(_ => _.IsSubclassOf(typeof(ConnectionHandlerBase)))
        .Select(_ => new DropdownItem<Type>
        {
          DisplayValue = _.Name,
          Value = _
        });
      foreach(var type in types)
      {
        _types.Add(type);
      }
      InitializeComponent();
    }

    private void OnPropertyChanged([CallerMemberName]string propertyName = null)
      => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public ObservableCollection<DropdownItem<Type>> Types
    {
      get
      {
        return _types;
      }
    }

    public DropdownItem<Type> SelectedItem
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

    public string ConnectionName
    {
      get
      {
        return _connectionName;
      }
      set
      {
        _connectionName = value;
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

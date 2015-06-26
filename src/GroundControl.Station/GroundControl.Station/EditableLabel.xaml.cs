using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace GroundControl.Station
{
  /// <summary>
  /// Interaction logic for EditableLabel.xaml
  /// </summary>
  public partial class EditableLabel : UserControl, INotifyPropertyChanged
  {
    private string _text;
    private bool _editMode;

    public EditableLabel()
    {
      Text = "Label";
      InitializeComponent();
    }

    public bool EditMode
    {
      get
      {
        return _editMode;
      }
      set
      {
        _editMode = value;
        OnPropertyChanged();
      }
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

    private void OnPropertyChanged([CallerMemberName]string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      if(e.Key == System.Windows.Input.Key.Enter)
      {
        EditMode = false;
      }
    }

    private void Label_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      EditMode = true;
    }
  }


 
}


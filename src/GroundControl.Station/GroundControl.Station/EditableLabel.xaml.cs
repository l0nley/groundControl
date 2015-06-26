using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GroundControl.Station
{
  /// <summary>
  /// Interaction logic for EditableLabel.xaml
  /// </summary>
  public partial class EditableLabel : UserControl
  {
    public static DependencyProperty IsReadonlyProperty = DependencyProperty.Register("IsReadonly", typeof(bool?), typeof(EditableLabel));
    public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(EditableLabel));
    public static DependencyProperty EditModeProperty = DependencyProperty.Register("EditMode", typeof(bool?), typeof(EditableLabel));

    public EditableLabel()
    {
      InitializeComponent();
    }

    public bool? EditMode
    {
      get
      {
        return (bool?)GetValue(EditModeProperty);
      }
      set
      {
        SetValue(EditModeProperty, value);
      }
    }

    public bool? IsReadonly
    {
      get
      {
        return (bool?)GetValue(IsReadonlyProperty);
      }

      set
      {
        SetValue(IsReadonlyProperty, value);
      }
    }

    public string Text
    {
      get
      {
        return (string)GetValue(TextProperty);
      }

      set
      {
        SetValue(TextProperty, value);
      }
    }
    private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      if(e.Key == Key.Enter)
      {
        EditMode = false;
      }
    }

    private void Label_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (IsReadonly.GetValueOrDefault() == false)
      {
        EditMode = true;
      }
    }
  }


 
}


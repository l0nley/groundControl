using GroundControl.Core;
using GroundControl.Station.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace GroundControl.Station.Components
{
  /// <summary>
  /// Interaction logic for MoveControl.xaml
  /// </summary>
  public partial class MoveControl : UserControl
  {
    private Dispatcher _dispatcher;
    public MoveControl()
    {
      _dispatcher = Dispatcher.CurrentDispatcher;
      InitializeComponent();
    }

    private MoveControlViewModel Model
    {
      get
      {
        return DataContext as MoveControlViewModel;
      }
    }

    private void MyKeyDown(KeyEventArgs e)
    {
      var model = _dispatcher.Invoke(() => Model);
      if (model.Harvester.NotOnline)
      {
        return;
      }
      var @char = Utillities.GetCharFromKey(e.Key);
      var cons = Task.Run(() => model.Harvester.Harvester.EnumerateConnections()).Result;
      var @byte = (byte)'0';
      switch (@char)
      {
        case '8':
        case 'w':
        case 'W':
          @byte = (byte)'8';
          Model.MoveType = MoveType.Forward;
          break;
        case '2':
        case 's':
        case 'S':
          @byte = (byte)'2';
          Model.MoveType = MoveType.Backward;
          break;
        case '4':
        case 'a':
        case 'A':
          @byte = (byte)'4';
          Model.MoveType = MoveType.Left;
          break;
        case '6':
        case 'd':
        case 'D':
          @byte = (byte)'6';
          Model.MoveType = MoveType.Right;
          break;
        case ' ':
        case '5':
          @byte = (byte)'5';
          Model.MoveType = MoveType.Stoped;
          break;
        case '+':
        case '-':
          @byte = (byte)@char;
          break;
      }

      if(@byte != (byte)'0')
      {
        SendCommand(@byte, cons, model.Harvester.Harvester);
      }

      e.Handled = true;
    }

    private void SendCommand(byte command,IEnumerable<IConnectionEndpoint> connections, DataHarvester harv)
    {
      var buf = new byte[] { command };
      foreach(var item in connections)
      {
        Task.Run(() => harv.SendCommand(buf, item));
      }
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
      Application.Current.MainWindow.KeyDown += MainWindow_KeyDown;
    }

    private void MainWindow_KeyDown(object sender, KeyEventArgs e)
    {
      MyKeyDown(e);
    }
  }
}

using System.Windows.Input;

namespace GroundControl.Station
{
  public static class Commands
  {
    private static RoutedUICommand NewProjectCommand = new RoutedUICommand("New Project", "New Project", typeof(Commands));
    private static RoutedUICommand SwitcherCommand = new RoutedUICommand("Switcher trigger", "Switcher trigger", typeof(Commands));

    public static RoutedUICommand Switcher
    {
      get
      {
        return SwitcherCommand;
      }
    }

    public static RoutedUICommand NewProject
    {
      get
      {
        return NewProjectCommand;
      }
    }
  }
}

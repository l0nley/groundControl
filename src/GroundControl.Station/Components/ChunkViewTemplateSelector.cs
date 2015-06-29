using GroundControl.Station.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace GroundControl.Station.Components
{
  public class ChunkViewTemplateSelector : DataTemplateSelector
  {
    public DataTemplate StringTemplate { get; set; }
    public DataTemplate LinearTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      var cvm = (item as ChunkViewModel);
      if(cvm == null)
      {
        return StringTemplate;
      }

      switch (cvm.ViewType)
      {
        case ChunkViewType.Linear:
          return LinearTemplate;
        default:
          return StringTemplate;
      }
    }
  }
}

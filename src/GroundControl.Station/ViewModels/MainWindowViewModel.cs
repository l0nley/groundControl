using System.Collections.ObjectModel;

namespace GroundControl.Station.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    public MainWindowViewModel()
    {
      _dataAgg = new ObservableCollection<DataAggregatorViewModel>();
    }

    private ObservableCollection<DataAggregatorViewModel> _dataAgg;

    public ObservableCollection<DataAggregatorViewModel> DataAggregators
    {
      get
      {
        return _dataAgg;
      }
    }
  }
}

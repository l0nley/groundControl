using System.Collections.ObjectModel;
using System.Linq;

namespace GroundControl.Station.Classes
{
  public class GroupContainer : LayoutElement
  {
    private string _caption;

    private ObservableCollection<LayoutElement> _elements;

    public ObservableCollection<LayoutElement> Elements
    {
      get { return _elements; }
      set
      {
        if (Equals(value, _elements)) return;
        _elements.CollectionChanged -= _elements_CollectionChanged;
        foreach (var item in _elements)
        {
          item.PropertyChanged -= Item_PropertyChanged;
        }
        _elements = value;
        foreach (var item in _elements)
        {
          item.PropertyChanged += Item_PropertyChanged;
        }
        _elements.CollectionChanged += _elements_CollectionChanged;
        OnPropertyChanged();
        OnPropertyChanged(nameof(Width));
        OnPropertyChanged(nameof(Height));
      }
    }

    private void _elements_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      foreach (LayoutElement item in e.OldItems)
      {
        item.PropertyChanged -= Item_PropertyChanged;
      }
      foreach (LayoutElement item in e.NewItems)
      {
        item.PropertyChanged += Item_PropertyChanged;
      }
      OnPropertyChanged(nameof(Width));
      OnPropertyChanged(nameof(Height));
    }

    private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case nameof(Width):
        case nameof(Height):
          OnPropertyChanged(e.PropertyName);
          break;
      }
    }

    public override int Width
    {
      get { return _elements.Sum(_ => _.Width); }
      set { }
    }

    public override int Height
    {
      get { return _elements.Sum(_ => _.Height); }
      set { }
    }

    public string Caption
    {
      get { return _caption; }
      set
      {
        if (value == _caption) return;
        _caption = value;
        OnPropertyChanged();
      }
    }
  }
}
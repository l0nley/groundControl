using GroundControl.Core;

namespace GroundControl.Station.ViewModels
{
  public class ChunkViewModel : ViewModelBase, IChunk
  {
    private IChunkDescription _description;
    private byte[] _value;
    private ChunkViewType _viewType;


    public ChunkViewType ViewType
    {
      get
      {
        return _viewType;
      }
      set
      {
        _viewType = value;
        OnPropertyChanged();
      }
    }

    public string HumanValue
    {
      get
      {
        return _description.ToHuman(Value);
      }
    }

    public IChunkDescription Description
    {
      get
      {
        return _description;
      }
      set
      {
        _description = value;
        OnPropertyChanged();
      }
    }

    public byte[] Value
    {
      get
      {
        return _value;
      }
      set
      {
        _value = value;
        OnPropertyChanged();
        OnPropertyChanged(nameof(HumanValue));
      }
    }
  }
}

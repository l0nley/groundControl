using GroundControl.Connections;
using GroundControl.Core;
using GroundControl.Protocols;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GroundControl.Station.ViewModels
{
  public class DataAggregatorViewModel : ViewModelBase
  {
    private readonly Dispatcher _dispatcher;
    private string _name;
    private readonly ObservableCollection<ConnectionViewModel> _connections;
    private readonly ObservableCollection<HealthItemViewModel> _health;
    private readonly ObservableCollection<ChunkViewModel> _chunkViews;
    private ConcurrentQueue<IChunk> _chunks;
    private DataReducer _reducer;
    private DataHarvester _harvester;
    private bool _online;

    public DataAggregatorViewModel()
    {
      _dispatcher = Dispatcher.CurrentDispatcher;
      _connections = new ObservableCollection<ConnectionViewModel>();
      _health = new ObservableCollection<HealthItemViewModel>();
      _chunkViews = new ObservableCollection<ChunkViewModel>();
    }

    public ConcurrentQueue<IChunk> Chunks
    {
      get
      {
        return _chunks;
      }
      set
      {
        _chunks = value;
        OnPropertyChanged();
      }
    }

    public bool Online
    {
      get
      {
        return _online;
      }
      set
      {
        if (_online == value)
        {
          return;
        }
        _online = value;
        if (value == true)
        {
          Start();
        }
        else
        {
          Stop();
        }
        OnPropertyChanged();
        OnPropertyChanged("NotOnline");
      }
    }

    public bool NotOnline
    {
      get
      {
        return _online == false;
      }
    }

    public string Name
    {
      get
      {
        return _name;
      }

      set
      {
        _name = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<ChunkViewModel> ChunkViews
    {
      get
      {
        return _chunkViews;
      }
    }

    public ObservableCollection<ConnectionViewModel> Connections
    {
      get
      {
        return _connections;
      }
    }

    public ObservableCollection<HealthItemViewModel> Health
    {
      get
      {
        return _health;
      }
    }

    public void Stop()
    {
      _reducer.HealthUpdated -= HealthUpdated;
      _reducer.OnFrame -= OnReducerFrame;
      _reducer.Dispose();
      _reducer = null;
      _harvester.HealthUpdated -= HealthUpdated;
      _harvester.Dispose();
      _harvester = null;
      _dispatcher.Invoke(() =>
      {
        _health.Clear();
      });
    }

    public void Start()
    {
      var factory = new ConnectionFactory(typeof(ConnectionHandlerBase)
        .Assembly
        .GetTypes()
        .Where(_ => _.IsSubclassOf(typeof(ConnectionHandlerBase)))
        .Select(_ => Activator.CreateInstance(_)).Cast<IConnectionHandler>());
      var descriptions = new ChunkDescriptionsRepository();
      descriptions.Add(new HumidityChunk());
      descriptions.Add(new TemperatureChunk());
      descriptions.Add(new DistanceChunk());
      _harvester = new DataHarvester(factory, _chunks, descriptions, 5);
      _harvester.HealthUpdated += HealthUpdated;
      foreach (var conn in Connections)
      {
        Task.Run(() => _harvester.CreateAndTrackConnection(new ConnectionEndpoint(conn.Uri))).Wait();
      }
      
      _reducer = new DataReducer(2, _chunks, 5);
      _reducer.OnFrame += OnReducerFrame;
      _reducer.HealthUpdated += HealthUpdated;
      _reducer.Start();
    }

    private void OnReducerFrame(IEnumerable<IChunk> frame)
    {
      foreach (var item in frame)
      {
        ChunkViewModel chunk = null;
        lock (ChunkViews)
        {
          chunk = ChunkViews.FirstOrDefault(_ => _.Description.Prefix == item.Description.Prefix);
        }

        if (chunk != null)
        {
          chunk.Value = item.Value;
        }
        else
        {
          _dispatcher.Invoke(() =>
          {
            ChunkViewModel a = null;
            lock (ChunkViews)
            {
              chunk = ChunkViews.FirstOrDefault(_ => _.Description.Prefix == item.Description.Prefix);
            }
            if (a != null)
            {
              return;
            }
            ChunkViews.Add(new ChunkViewModel
            {

              Description = item.Description,
              Value = item.Value,
              ViewType = item.Description.Prefix == (byte)'D' ? ChunkViewType.Linear : ChunkViewType.Value
            });
          });
        }
      }
    }

    private void HealthUpdated(object sender, IEnumerable<IHealthDescription> descriptions)
    {
      foreach (var item in descriptions)
      {
        HealthItemViewModel hlth;
        lock (Health)
        {
          hlth = Health.FirstOrDefault(_ => _.Name == item.Name);
        }
        if (hlth != null)
        {
          hlth.Value = item.Value;
        }
        else
        {

          _dispatcher.Invoke(() =>
          {
            HealthItemViewModel a;
            lock (Health)
            {
              a = Health.FirstOrDefault(_ => _.Name == item.Name);
            }
            if (a != null)
            {
              return;
            }
            _health.Add(new HealthItemViewModel
            {
              Name = item.Name,
              Value = item.Value
            });
          });
        }
      }
    }
  }
}

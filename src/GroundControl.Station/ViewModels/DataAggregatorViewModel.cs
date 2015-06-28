using GroundControl.Connections;
using GroundControl.Core;
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
    private ConcurrentQueue<IChunk> _chunks;
    private DataHarvester _harvester;
    private bool _online;

    public DataAggregatorViewModel()
    {
      _dispatcher = Dispatcher.CurrentDispatcher;
      _connections = new ObservableCollection<ConnectionViewModel>();
      _health = new ObservableCollection<HealthItemViewModel>();
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
        if(value == true)
        {
          Start();
        } else
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
      _harvester.HealthUpdated -= HealthUpdated;
      _harvester.Dispose();
      _harvester = null;
    }

    public void Start()
    {
      var factory = new ConnectionFactory(typeof(ConnectionHandlerBase)
        .Assembly
        .GetTypes()
        .Where(_ => _.IsSubclassOf(typeof(ConnectionHandlerBase)))
        .Select(_ => Activator.CreateInstance(_)).Cast<IConnectionHandler>());
      var descriptions = new ChunkDescriptionsRepository();
      _harvester = new DataHarvester(factory, _chunks, descriptions, 5);
      _harvester.HealthUpdated += HealthUpdated;
      foreach(var conn in Connections)
      {
        Task.Run(() => _harvester.CreateAndTrackConnection(new ConnectionEndpoint(conn.Uri))).Wait();
      }
    }

    private void HealthUpdated(object sender, IEnumerable<IHealthDescription> descriptions)
    {
      foreach(var item in descriptions)
      {
        var hlth = Health.FirstOrDefault(_ => _.Name == item.Name);
        if(hlth != null)
        {
          hlth.Value = item.Value;
        }
        else
        {
          _dispatcher.Invoke(() =>
          Health.Add(new HealthItemViewModel
          {
            Name = item.Name,
            Value = item.Value
          }));
        }
      }
    }
  }
}

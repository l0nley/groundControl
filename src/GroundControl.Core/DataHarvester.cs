using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroundControl.Core
{
  public class DataHarvester : HealthDataProviderBase, IDataHarvester
  {
    private const string TotalBytes = "Total Bytes";
    private const string TotalChunks = "Total Chunks";
    private const string QueueState = "Chunk Queue";
    private const string CommandsState = "Commands Sended";
    private readonly IConnectionFactory _factory;
    private readonly ConcurrentQueue<IChunk> _packetQueue;
    private readonly IChunkDescriptionsRepository _descriptionsRepository;
    private volatile bool _repositoryChanged;
    private long _totalBytes;
    private long _totalChunks;
    private long _commandsSended;
    private readonly ConcurrentDictionary<IConnectionEndpoint, Tuple<Connection, Thread, ConcurrentQueue<byte[]>>> _connections;

    public DataHarvester(
      IConnectionFactory factory,
      ConcurrentQueue<IChunk> packetQueue,
      IChunkDescriptionsRepository descriptionsRepository,
      int secondsToUpdateHealth = 0)
      : base(secondsToUpdateHealth)
    {
      _connections = new ConcurrentDictionary<IConnectionEndpoint, Tuple<Connection, Thread, ConcurrentQueue<byte[]>>>();
      _commandsSended = 0;
      _factory = factory;
      _packetQueue = packetQueue;
      _repositoryChanged = false;
      _totalBytes = 0;
      _totalChunks = 0;
      _descriptionsRepository = descriptionsRepository;
      _descriptionsRepository.RepositoryChanged += RepositoryUpdate;
    }

    /// <summary>
    /// Creates and tracks connection
    /// </summary>
    /// <param name="endpoint">Connection endpoint</param>
    /// <returns>The awaiter</returns>
    public async Task CreateAndTrackConnection(IConnectionEndpoint endpoint)
    {
      var connection = await _factory.Create(endpoint);
      var thread = new Thread(Worker);
      var queue = new ConcurrentQueue<byte[]>();
      _connections.AddOrUpdate(endpoint, new Tuple<Connection, Thread, ConcurrentQueue<byte[]>>(connection, thread, queue), (a, b) => b);
      await connection.Open();

      thread.Start(new Tuple<Connection, ConcurrentQueue<byte[]>>(connection, queue));
    }

    /// <summary>
    /// Drops connection
    /// </summary>
    /// <param name="endpoint">Connection endpoint</param>
    /// <returns>The awaiter</returns>
    public void DropConnection(IConnectionEndpoint endpoint)
    {
      Tuple<Connection, Thread, ConcurrentQueue<byte[]>> value;
      if (_connections.TryRemove(endpoint, out value) == false)
      {
        return;
      }

      value.Item2.Abort();
      value.Item2.Join();
      value.Item1.Dispose();
    }

    /// <summary>
    /// Enumerates connection
    /// </summary>
    /// <returns>Connections collection</returns>
    public Task<IEnumerable<IConnectionEndpoint>> EnumerateConnections()
    {
      return Task.FromResult((IEnumerable<IConnectionEndpoint>)_connections.Keys.ToArray());
    }

    /// <summary>
    /// Porepares descriptions for reporing
    /// </summary>
    /// <returns>Collection of descriptions</returns>
    protected override ICollection<IHealthDescription> PrepareDescriptions()
    {
      return new IHealthDescription[]
      {
        new HealthDescription(TotalBytes, _totalBytes),
        new HealthDescription(TotalChunks, _totalChunks),
        new HealthDescription(QueueState, _packetQueue.Count),
        new HealthDescription(CommandsState, _commandsSended)
      };
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public override void Dispose()
    {
      _descriptionsRepository.RepositoryChanged -= RepositoryUpdate;
      var values = _connections.Values.ToArray();
      _connections.Clear();
      foreach (var connection in values)
      {
        try
        {
          connection.Item2.Abort();
          connection.Item2.Join();
          connection.Item1.Dispose();
        }
        catch (Exception)
        {
          // ignored
        }
      }

      base.Dispose();
    }

    /// <summary>
    /// Updates repo in cache
    /// </summary>
    private void RepositoryUpdate(object sender)
    {
      _repositoryChanged = true;
    }

    /// <summary>
    /// Main worker
    /// </summary>
    /// <param name="state"></param>
    private void Worker(object state)
    {
      var tuple = (Tuple<Connection, ConcurrentQueue<byte[]>>)state;
      var repositoryCache = (IDictionary<byte, IChunkDescription>)_descriptionsRepository.Clone();
      try
      {
        while (true)
        {
          if (_repositoryChanged)
          {
            _repositoryChanged = false;
            repositoryCache = (IDictionary<byte, IChunkDescription>)_descriptionsRepository.Clone();
          }

          if (tuple.Item2.Count> 0)
          {
            var command = tuple.Item2.DequeueNotMore(tuple.Item2.Count);
            if (command.Count > 0)
            {
              tuple.Item1.Write(command[0], 0, command[0].Length);
              Interlocked.Increment(ref _commandsSended);
            }
          }

          var signature = tuple.Item1.ReadByte();
          if (signature < 0)
          {
            continue;
          }
          Interlocked.Increment(ref _totalBytes);
          IChunkDescription description;
          if (repositoryCache.TryGetValue((byte)signature, out description) == false)
          {
            continue;
          }

          var chunk = Task.Run(() => ReadChunk(tuple.Item1, description)).Result;
          Interlocked.Increment(ref _totalChunks);
          _packetQueue.Enqueue(chunk);
        }
      }
      catch (ThreadAbortException)
      {
        // ignored
      }
    }

    /// <summary>
    /// Reads particular chunk from connection
    /// </summary>
    /// <param name="connection">the Connection</param>
    /// <param name="description">The description</param>
    /// <returns>Readed chunk</returns>
    private async Task<IChunk> ReadChunk(Stream connection, IChunkDescription description)
    {
      if (description.FixedSize)
      {
        var buffer = new byte[description.ByteLength];
        var totalReaded = 0;
        while (totalReaded < description.ByteLength)
        {
          var i = totalReaded;
          var readed = await connection.ReadAsync(buffer, i, buffer.Length - i);
          totalReaded += readed;
        }

        Interlocked.Add(ref _totalBytes, totalReaded);
        return new Chunk(description, buffer);
      }

      var buffers = new List<byte>();
      while (true)
      {
        var readed = connection.ReadByte();
        if (readed <= 0)
        {
          continue;
        }
        Interlocked.Increment(ref _totalBytes);
        if (readed == description.Terminator)
        {
          return new Chunk(description, buffers.ToArray());
        }
        buffers.Add((byte)readed);
      }
    }
    /// <summary>
    /// Sens command to corresponding endpoint
    /// </summary>
    /// <param name="command">The command</param>
    /// <param name="endpoint">The endpoint</param>
    /// <returns>Task to wait</returns>
    public Task SendCommand(byte[] command, IConnectionEndpoint endpoint)
    {
      Tuple<Connection, Thread, ConcurrentQueue<byte[]>> item;
      if(_connections.TryGetValue(endpoint,out item) == false)
      {
        throw new InvalidOperationException();
      }

      item.Item3.Enqueue(command);
      return Task.FromResult(0);
    }
  }
}
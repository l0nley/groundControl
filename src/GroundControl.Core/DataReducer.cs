using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroundControl.Core
{
  /// <summary>
  /// Redicer and processor
  /// </summary>
  public class DataReducer : HealthDataProviderBase, IDataReducer
  {
    private const string ChunksPerFrame = "Chunks/Frame";
    private const string TotalFrames = "Total Frames";
    private long _totalFrames;
    private double _chunksPerFrame;
    private readonly ConcurrentQueue<IChunk> _packetQueue;
    private readonly Thread _queueProcessor;
    private readonly AutoResetEvent _autoReset;
    private Timer _timer;
    public DataReducer(int framesPerSecond, ConcurrentQueue<IChunk> packetQueue, int secondsToUpdateHealth = 0)
      :base(secondsToUpdateHealth)
    {
      if (framesPerSecond <= 0)
      {
        throw new ArgumentException("Should be greater than zero", nameof(framesPerSecond));
      }
      _totalFrames = 0;
      _autoReset = new AutoResetEvent(false);
      _packetQueue = packetQueue;
      FramesPerSecond = framesPerSecond;
      _queueProcessor = new Thread(QueueWorker);
      _chunksPerFrame = 0;
      IsRunning = false;
    }

    /// <summary>
    /// Frames per second to produce
    /// </summary>
    public int FramesPerSecond { get; }

    /// <summary>
    /// Starts reducing job
    /// </summary>
    public Task Start()
    {
      if (IsRunning)
      {
        throw new InvalidOperationException();
      }
      _timer = new Timer(TimerCallback, null, 0, 1000/FramesPerSecond);
      _queueProcessor.Start();
      IsRunning = true;
      return Task.FromResult(0);
    }

    /// <summary>
    /// Is running queue or not
    /// </summary>
    public bool IsRunning { get; private set; }

    /// <summary>
    /// Stops reducing job
    /// </summary>
    public Task Stop()
    {
      if (IsRunning == false)
      {
        throw new InvalidOperationException();
      }
      IsRunning = false;
      _timer.Change(0, Timeout.Infinite);
      _timer.Dispose();
      _timer = null;
      _queueProcessor.Abort();
      _queueProcessor.Join();
      return Task.FromResult(0);
    }

    /// <summary>
    /// On frame arrived
    /// </summary>
    public event FrameDelegate OnFrame;

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public override void Dispose()
    {
      if (IsRunning)
      {
        Task.Run(() => Stop()).Wait();
      }

      base.Dispose();
    }

    /// <summary>
    /// Timer callback
    /// </summary>
    /// <param name="state">The state</param>
    private void TimerCallback(object state)
    {
      _autoReset.Set();
    }

    protected override ICollection<IHealthDescription> PrepareDescriptions()
    {
      return new List<IHealthDescription>
      {
        new HealthDescription(ChunksPerFrame, _chunksPerFrame),
        new HealthDescription(TotalFrames, _totalFrames)
      };
    }

    /// <summary>
    /// Queue worker delegate
    /// </summary>
    /// <param name="state">State object</param>
    private void QueueWorker(object state)
    {
      var lcts = new LimitedConcurrencyLevelTaskScheduler(1);
      var factory = new TaskFactory(lcts);
      try
      {
        while (true)
        {
          _autoReset.WaitOne();
          var currentBatch = _packetQueue.DequeueNotMore(_packetQueue.Count);
          _chunksPerFrame = (_chunksPerFrame + currentBatch.Count)/2.0;
          Interlocked.Increment(ref _totalFrames);
          if (currentBatch.Count > 0)
          {
            factory.StartNew(Reduce, currentBatch);
          }
        }
      }
      catch (ThreadAbortException)
      {
        // ignored
      }
    }

    /// <summary>
    /// Reduces packets and run event
    /// </summary>
    /// <param name="state">object state</param>
    private void Reduce(object state)
    {
      var packets = (List<IChunk>) state;
      var packet = ReduceChunks(packets);
      OnFrame?.Invoke(packet);
    }

    /// <summary>
    /// Reduces chunk batch
    /// </summary>
    /// <param name="chunks">Chunks to reduce</param>
    /// <returns></returns>
    private static IEnumerable<IChunk> ReduceChunks(IEnumerable<IChunk> chunks)
    {
      var grouped = chunks.GroupBy(_ => _.Description);
      return (from chunkGroup 
              in grouped let aggregated = chunkGroup.Key.Aggregate(chunkGroup.Select(_ => _.Value))
              select (IChunk)new Chunk(chunkGroup.Key, aggregated)).ToList();
    }
  }
}
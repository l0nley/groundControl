using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroundControl.Core
{
  /// <summary>
  /// Data reducer
  /// </summary>
  public interface IDataReducer : IDisposable
  {
    /// <summary>
    /// Frames per second to produce
    /// </summary>
    int FramesPerSecond { get; }

    /// <summary>
    /// Starts reducing job
    /// </summary>
    Task Start();

    /// <summary>
    /// Stops reducing job
    /// </summary>
    Task Stop();

    /// <summary>
    /// Is running queue or not
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    /// On frame arrived
    /// </summary>
    event FrameDelegate OnFrame;
  }

  public  delegate void FrameDelegate(IEnumerable<IChunk> frame);
}
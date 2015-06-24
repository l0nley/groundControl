using System;
using System.Collections.Generic;
using System.Threading;

namespace GroundControl.Core
{
  /// <summary>
  /// Health data provider base
  /// </summary>
  public abstract class HealthDataProviderBase : IHealthDataProvider
  {
    private readonly Timer _timer;

    protected HealthDataProviderBase(int secondsBetweenUpdates)
    {
      if (secondsBetweenUpdates == 0)
      {
        return;
      }

      _timer = new Timer(TimerCallback, null, secondsBetweenUpdates * 1000, secondsBetweenUpdates * 1000);
    }

    /// <summary>
    /// Health update timer callback
    /// </summary>
    /// <param name="state">Timer state</param>
    private void TimerCallback(object state)
    {
      var info = PrepareDescriptions();
      if (info.Count > 0)
      {
        HealthUpdated?.Invoke(this, info);
      }
    }

    /// <summary>
    /// Porepares descriptions for reporing
    /// </summary>
    /// <returns>Collection of descriptions</returns>
    protected abstract ICollection<IHealthDescription> PrepareDescriptions();

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public virtual void Dispose()
    {
      try
      {
        _timer.Dispose();
      }
      catch (Exception)
      {
        // ignored
      }
    }

    /// <summary>
    /// Health updated event
    /// </summary>
    public event HealthStatusUpdatedDelegate HealthUpdated;
  }
}
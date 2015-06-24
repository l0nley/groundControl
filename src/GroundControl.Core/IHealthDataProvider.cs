using System;
using System.Collections.Generic;

namespace GroundControl.Core
{
  /// <summary>
  /// Health information provider
  /// </summary>
  public interface IHealthDataProvider : IDisposable
  {
    /// <summary>
    /// Health information was updated
    /// </summary>
    event HealthStatusUpdatedDelegate HealthUpdated;
  }

  /// <summary>
  /// Health information was updated
  /// </summary>
  public delegate void HealthStatusUpdatedDelegate(object sender, IEnumerable<IHealthDescription> descriptions);
}
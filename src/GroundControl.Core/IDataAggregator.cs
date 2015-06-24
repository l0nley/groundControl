using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroundControl.Core
{
  /// <summary>
  /// Data aggregator
  /// </summary>
  public interface IDataAggregator : IDisposable
  {
    /// <summary>
    /// Creates and tracks connection
    /// </summary>
    /// <param name="endpoint">Connection endpoint</param>
    /// <returns>The awaiter</returns>
    Task CreateAndTrackConnection(IConnectionEndpoint endpoint);

    /// <summary>
    /// Drops connection
    /// </summary>
    /// <param name="endpoint">Connection endpoint</param>
    /// <returns>The awaiter</returns>
    void DropConnection(IConnectionEndpoint endpoint);

    /// <summary>
    /// Enumerates connection
    /// </summary>
    /// <returns>Connections collection</returns>
    Task<IEnumerable<IConnectionEndpoint>> EnumerateConnections();
  }
}
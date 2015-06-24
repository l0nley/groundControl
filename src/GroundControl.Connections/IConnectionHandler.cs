using System.Threading.Tasks;
using GroundControl.Core;

namespace GroundControl.Connections
{
  /// <summary>
  /// Connection handler
  /// </summary>
  public interface IConnectionHandler
  {
    /// <summary>
    /// Can handle uri
    /// </summary>
    /// <param name="uri">string with uri</param>
    /// <returns>Can or not</returns>
    bool CanHandle(string uri);

    /// <summary>
    /// Creates connection
    /// </summary>
    /// <param name="endpoint">Endpoint for connection</param>
    /// <returns>The connection</returns>
    Task<Connection> CreateConnection(IConnectionEndpoint endpoint);
  }
}
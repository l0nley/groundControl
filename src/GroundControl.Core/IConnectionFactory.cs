using System.Threading.Tasks;

namespace GroundControl.Core
{
  /// <summary>
  /// Connection factory
  /// </summary>
  public interface IConnectionFactory : IRepositoryChangedNotifier
  {
    /// <summary>
    /// Creates new connection
    /// </summary>
    /// <param name="endpoint">Enpoint for connection</param>
    /// <returns>Awaiter for connection</returns>
    Task<Connection> Create(IConnectionEndpoint endpoint);
  }
}
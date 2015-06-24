using System.IO;
using System.Threading.Tasks;

namespace GroundControl.Core
{
  /// <summary>
  /// Describes connection
  /// </summary>
  public abstract class Connection : Stream
  {
    protected Connection(IConnectionEndpoint endpoint)
    {
      Endpoint = endpoint;
    }

    /// <summary>
    /// Opens stream for communication
    /// </summary>
    /// <returns>The task</returns>
    public abstract Task Open();

    /// <summary>
    /// Indicates is stream opened;
    /// </summary>
    public abstract bool IsOpened { get; protected set; }

    /// <summary>
    /// Endpoint for connection
    /// </summary>
    public IConnectionEndpoint Endpoint { get; }
  }
}
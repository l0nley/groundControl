using GroundControl.Core;

namespace GroundControl.Connections
{
  /// <summary>
  /// Simple connection endpoint
  /// </summary>
  public class ConnectionEndpoint : IConnectionEndpoint
  {
    public ConnectionEndpoint(string uri)
    {
      Uri = uri;
    }

    /// <summary>
    /// Uri to connect
    /// </summary>
    public string Uri { get; }
  }
}
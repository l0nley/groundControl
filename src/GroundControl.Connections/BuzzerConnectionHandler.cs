using System;
using System.Threading.Tasks;
using GroundControl.Core;

namespace GroundControl.Connections
{
  public class BuzzerConnectionHandler : ConnectionHandlerBase
  {
    public BuzzerConnectionHandler() : base(@"^device://buzzer[\d]+$")
    {
    }

    /// <summary>
    /// Creates connection
    /// </summary>
    /// <param name="endpoint">Endpoint for connection</param>
    /// <returns>The connection</returns>
    public override Task<Connection> CreateConnection(IConnectionEndpoint endpoint)
    {
      if (CanHandle(endpoint.Uri) == false)
      {
        throw new NotSupportedException(NotSupporterUri);
      }

      return Task.FromResult((Connection) new BuzzerConnection(endpoint));
    }
  }
}
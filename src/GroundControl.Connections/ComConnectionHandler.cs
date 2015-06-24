using System;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GroundControl.Core;

namespace GroundControl.Connections
{
  public class ComConnectionHandler : ConnectionHandlerBase
  {
    private static readonly Regex PortExtractor = new Regex(@"COM[\d]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
    public ComConnectionHandler() : base(@"^device://COM[\d]+$")
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

      var extracted = PortExtractor.Matches(endpoint.Uri)[0].Value;
      return Task.FromResult((Connection)new ComConnection(endpoint, extracted));
    }
  }
}
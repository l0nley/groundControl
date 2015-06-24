using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GroundControl.Core;

namespace GroundControl.Connections
{
  /// <summary>
  /// Connection handler base
  /// </summary>
  public abstract class ConnectionHandlerBase : IConnectionHandler
  {
    protected const string NotSupporterUri = "Specified endpoint URI is not supported";
    protected readonly Regex MatchRegex;
    protected ConnectionHandlerBase(string matchRegex)
    {
      MatchRegex = new Regex(matchRegex, RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// Can handle uri
    /// </summary>
    /// <param name="uri">string with uri</param>
    /// <returns>Can or not</returns>
    public bool CanHandle(string uri)
    {
      return MatchRegex.IsMatch(uri);
    }

    /// <summary>
    /// Creates connection
    /// </summary>
    /// <param name="endpoint">Endpoint for connection</param>
    /// <returns>The connection</returns>
    public abstract Task<Connection> CreateConnection(IConnectionEndpoint endpoint);
  }
}
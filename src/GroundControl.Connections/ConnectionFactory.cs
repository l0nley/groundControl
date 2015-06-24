using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroundControl.Core;

namespace GroundControl.Connections
{
  public class ConnectionFactory : IConnectionFactory
  {
    private readonly ConcurrentBag<IConnectionHandler> _connectionHandlers;
    public ConnectionFactory(IEnumerable<IConnectionHandler> handlers = null)
    {
      if (handlers != null)
      {
        _connectionHandlers = new ConcurrentBag<IConnectionHandler>(handlers);
        Task.Run(() => RepositoryChanged?.Invoke(this));
      }
      else
      {
        _connectionHandlers = new ConcurrentBag<IConnectionHandler>();
      }
      
    }

    /// <summary>
    /// Adds connection handler to repository
    /// </summary>
    /// <param name="handler">The handler</param>
    public void AddHandler(IConnectionHandler handler)
    {
      if (handler == null)
      {
        throw new ArgumentNullException(nameof(handler));
      }

      _connectionHandlers.Add(handler);
      RepositoryChanged?.Invoke(this);
    }

    /// <summary>
    /// Creates new connection
    /// </summary>
    /// <param name="endpoint">Enpoint for connection</param>
    /// <returns>Awaiter for connection</returns>
    public Task<Connection> Create(IConnectionEndpoint endpoint)
    {
      var handler = _connectionHandlers.FirstOrDefault(_ => _.CanHandle(endpoint.Uri));
      if (handler == null)
      {
        throw new InvalidOperationException("Cannot create connection: handler for URI format not found");
      }
      return handler.CreateConnection(endpoint);
    }

    public event RepositoryChangedDelegate RepositoryChanged;
  }
}
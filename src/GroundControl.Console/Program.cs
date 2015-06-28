using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroundControl.Connections;
using GroundControl.Core;
using GroundControl.Protocols;

namespace GroundControl.Console
{
  public class Program
  {
    public static void Main(string[] args)
    {
      System.Console.SetWindowSize(85,43);
      MainAsync().Wait();
    }

    public static async Task MainAsync()
    {
      var repo = new ChunkDescriptionsRepository
      {
        new HumidityChunk(),
        new DistanceChunk(),
        new TemperatureChunk()
      };

      var queue = new ConcurrentQueue<IChunk>();
      var connectionFactory = new ConnectionFactory();
      connectionFactory.AddHandler(new ComConnectionHandler());
      connectionFactory.AddHandler(new BuzzerConnectionHandler());

      using (var aggregator = new DataHarvester(connectionFactory, queue, repo, 5))
      {
        aggregator.HealthUpdated += HealthUpdated;
        using (var reducer = new DataReducer(1, queue, 5))
        {
          reducer.HealthUpdated += HealthUpdated;
          reducer.OnFrame += Reducer_OnFrame;
          await reducer.Start();
          await aggregator.CreateAndTrackConnection(new ConnectionEndpoint("device://buzzer1"));
          System.Console.WriteLine("Press enter to exit...");
          System.Console.ReadLine();
          reducer.OnFrame -= Reducer_OnFrame;
          reducer.HealthUpdated -= HealthUpdated;
        }
        aggregator.HealthUpdated -= HealthUpdated;
      }
    }

    private static void Reducer_OnFrame(IEnumerable<IChunk> enumerable)
    {
      System.Console.WriteLine(string.Join(" ", enumerable.Select(_=>_.ToString())));
    }

    private static void HealthUpdated(object sender, IEnumerable<IHealthDescription> descriptions)
    {
      System.Console.WriteLine("HEALTH {0} > {1}", sender.GetType().Name,
        string.Join(",", descriptions.Select(_ => $"{_.Name}:{_.Value}")));
    }
  }
}

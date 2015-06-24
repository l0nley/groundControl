using System.Collections.Concurrent;
using System.Collections.Generic;

namespace GroundControl.Core
{
  /// <summary>
  /// Usefull extensions
  /// </summary>
  public static class Helpers
  {
    /// <summary>
    /// Take not more than count from queue
    /// </summary>
    /// <typeparam name="T">type of item in queue</typeparam>
    /// <param name="queue">The queue</param>
    /// <param name="count">The count</param>
    /// <returns>dequed items</returns>
    public static IList<T> DequeueNotMore<T>(this ConcurrentQueue<T> queue, int count)
    {
      var list = new List<T>();
      while (list.Count <= count)
      {
        T item;
        if (queue.TryDequeue(out item) == false)
        {
          return list;
        }

        list.Add(item);
      }

      return list;
    }

    /// <summary>
    /// Enqueues item range
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="queue">The queue</param>
    /// <param name="items">The items</param>
    public static void EnqueueRange<T>(this ConcurrentQueue<T> queue, IEnumerable<T> items)
    {
      foreach (var item in items)
      {
        queue.Enqueue(item);
      }
    }
  }
}
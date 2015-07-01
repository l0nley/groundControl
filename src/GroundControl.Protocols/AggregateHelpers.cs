using System;
using System.Collections.Generic;
using System.Linq;

namespace GroundControl.Protocols
{
  /// <summary>
  /// Aggregate helpers
  /// </summary>
  public static class AggregateHelpers
  {
    /// <summary>
    /// Aggregates values as average float value
    /// </summary>
    /// <param name="chunks">Chunks to aggregate</param>
    /// <returns>Aggregated value</returns>
    public static byte[] AverageFloat(IEnumerable<byte[]> chunks) =>
      BitConverter.GetBytes(chunks.Select(_ => BitConverter.ToSingle(_, 0)).Average());

    /// <summary>
    /// Aggregates values as average float value
    /// </summary>
    /// <param name="chunks">Chunks to aggregate</param>
    /// <returns>Aggregated value</returns>
    public static byte[] AverageShort(IEnumerable<byte[]> chunks)
    {
      var sum = 0;
      var count = 0;
      foreach(var item in chunks)
      {
        sum += BitConverter.ToInt16(item, 0);
        count++;
      }

      var avg = (short)(sum / count);
      return BitConverter.GetBytes(avg);
    }

    /// <summary>
    /// Aggregates values as average float value
    /// </summary>
    /// <param name="chunks">Chunks to aggregate</param>
    /// <returns>Aggregated value</returns>
    public static byte[] AverageByte(IEnumerable<byte[]> chunks)
    {
      var sum = 0;
      var count = 0;
      foreach (var item in chunks)
      {
        sum += item[0];
        count++;
      }

      var avg = (byte)(sum / count);
      return new[] { avg };
    }
  }
}
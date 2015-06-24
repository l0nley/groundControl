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
  }
}
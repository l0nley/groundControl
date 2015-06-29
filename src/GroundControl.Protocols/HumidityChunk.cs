using System;
using System.Globalization;
using GroundControl.Core;

namespace GroundControl.Protocols
{
  /// <summary>
  /// Chunk description for humidity
  /// </summary>
  public class HumidityChunk : FixedSizeChunkBase
  {
    public HumidityChunk() 
      :base((byte)'H',sizeof(float), AggregateHelpers.AverageFloat)
    {
      Description = "Humidity";
    }

    /// <summary>
    /// Converts value of chunk to human readble format
    /// </summary>
    /// <param name="value">The value</param>
    /// <returns>the string</returns>
    public override string ToHuman(byte[] value) => BitConverter.ToSingle(value, 0).ToString("000.0 %",CultureInfo.InvariantCulture);
  }
}

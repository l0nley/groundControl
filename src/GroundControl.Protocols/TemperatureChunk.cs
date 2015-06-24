using System;
using System.Globalization;

namespace GroundControl.Protocols
{
  /// <summary>
  /// Chunk description for temperature
  /// </summary>
  public class TemperatureChunk : FixedSizeChunkBase
  {
    public TemperatureChunk()
      : base((byte)'T', sizeof(float), AggregateHelpers.AverageFloat)
    {
      Description = "Temperature, C*";
    }

    /// <summary>
    /// Converts value of chunk to human readble format
    /// </summary>
    /// <param name="value">The value</param>
    /// <returns>the string</returns>
    public override string ToHuman(byte[] value) => BitConverter.ToSingle(value, 0).ToString(CultureInfo.InvariantCulture);
  }
}